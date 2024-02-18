
document.addEventListener('DOMContentLoaded', function () {
    
    var deleteButtons = document.querySelectorAll('.delete-button');
    deleteButtons.forEach(function (button) {
        button.addEventListener('click', function (e) {
            e.preventDefault();

            
            var alunoId = this.getAttribute('data-aluno-id');

            
            var deleteButton = document.getElementById('deleteButton');
            deleteButton.setAttribute('href', '/Aluno/Excluir/' + alunoId);

            
            var confirmDeleteModalElement = document.getElementById('confirmDeleteModal');
            var confirmDeleteModal = new bootstrap.Modal(confirmDeleteModalElement);
            confirmDeleteModal.show();
        });
    });

    
    var closeButtons = document.querySelectorAll('.close');
    closeButtons.forEach(function (button) {
        button.addEventListener('click', function (e) {
            e.preventDefault();

            
            window.location.href = '/Aluno';
        });
    });
});

$(document).ready(function () {
    $("#myButton").click(function () {
        $(this).html('<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> Carregando...');
    });
});
