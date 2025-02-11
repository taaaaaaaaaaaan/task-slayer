function openModal(id, title, description, category, status, date) {
    let modal = document.getElementById("taskModal");
    document.getElementById("modal-title").innerText = title;
    document.getElementById("modal-description").innerText = description;
    document.getElementById("modal-category").innerText = category;
    document.getElementById("modal-status").innerText = status;
    document.getElementById("modal-date").innerText = date;
    
    modal.style.display = "flex";
}

function closeModal() {
    document.getElementById("taskModal").style.display = "none";
}