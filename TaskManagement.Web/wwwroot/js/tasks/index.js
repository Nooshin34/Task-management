document.querySelectorAll('[data-bs-toggle="tooltip"]').forEach(function (el) {
    bootstrap.Tooltip.getOrCreateInstance(el);
});

let pendingTaskId = null;
const modalElement = document.getElementById("deleteModal");
const confirmBtn = document.getElementById("confirmDeleteBtn");
const modal = modalElement ? bootstrap.Modal.getOrCreateInstance(modalElement) : null;

document.addEventListener("click", function (e) {
    const btn = e.target.closest(".btn-delete-task");
    if (!btn) {
        return;
    }

    pendingTaskId = btn.getAttribute("data-task-id");
    if (!pendingTaskId || !modal) {
        return;
    }

    const tooltip = bootstrap.Tooltip.getInstance(btn);
    if (tooltip) {
        tooltip.hide();
    }

    modal.show();
});

if (confirmBtn) {
    confirmBtn.addEventListener("click", function () {
        const taskId = pendingTaskId;
        if (!taskId) {
            return;
        }

        const tokenInput = document.querySelector('input[name="__RequestVerificationToken"]');
        const deleteUrl = confirmBtn.getAttribute("data-delete-url");
        if (!tokenInput || !deleteUrl) {
            return;
        }

        confirmBtn.disabled = true;

        fetch(deleteUrl, {
            method: "POST",
            headers: {
                "Content-Type": "application/x-www-form-urlencoded; charset=UTF-8",
                RequestVerificationToken: tokenInput.value
            },
            body: new URLSearchParams({
                id: taskId,
                __RequestVerificationToken: tokenInput.value
            }).toString()
        })
            .then(function (response) {
                if (!response.ok) {
                    throw new Error("delete-failed");
                }

                return response.json();
            })
            .then(function (data) {
                if (!data || !data.success) {
                    throw new Error(data && data.message ? data.message : "delete-failed");
                }

                markRowDeleted(taskId);
                if (modal) {
                    modal.hide();
                }
            })
            .catch(function () {
                window.alert("حذف انجام نشد. دوباره تلاش کنید.");
            })
            .finally(function () {
                confirmBtn.disabled = false;
                pendingTaskId = null;
            });
    });
}

function markRowDeleted(taskId) {
    const row = document.querySelector('tr[data-task-id="' + taskId + '"]');
    if (!row) {
        return;
    }

    row.querySelectorAll('[data-bs-toggle="tooltip"]').forEach(function (el) {
        const tooltip = bootstrap.Tooltip.getInstance(el);
        if (tooltip) {
            tooltip.dispose();
        }
    });

    row.classList.add("task-deleted");

    const statusCell = row.querySelector(".task-status");
    if (statusCell) {
        statusCell.innerHTML = '<span class="badge task-deleted-badge">حذف‌شده</span>';
    }

    const actionsCell = row.querySelector(".task-actions");
    if (actionsCell) {
        actionsCell.innerHTML = "";
    }
}
