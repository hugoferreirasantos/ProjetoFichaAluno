document.addEventListener('DOMContentLoaded', function () {
    var showSuccessModal = '@TempData["ShowSuccessModal"]';
    if (showSuccessModal === 'True') {
        var successModalElement = document.getElementById('successModal');
        var successModal = new bootstrap.Modal(successModalElement);
        successModal.show();
    }
});

document.addEventListener('DOMContentLoaded', function () {
    var showErrorModal = '@TempData["ShowErrorModal"]';
    if (showErrorModal === 'True') {
        var errorModalElement = document.getElementById('errorModal');
        var errorModal = new bootstrap.Modal(errorModalElement);
        errorModal.show();
    }
});

document.addEventListener('DOMContentLoaded', function () {
    var showSuccessModal = '@TempData["ShowErrorModalNome"]';
    if (showSuccessModal === 'True') {
        var successModalElement = document.getElementById('erroNome');
        var successModal = new bootstrap.Modal(successModalElement);
        successModal.show();
    }
});


var input = document.getElementById('NASCIMENTO');
input.addEventListener('input', function (e) {
    this.type = 'text';
    if (this.value.length == 2 || this.value.length == 5) {
        this.value += '/';
    }
});

input.addEventListener('blur', function (e) {
    this.type = 'text';
    var match = this.value.match(/^(\d{2})\/(\d{2})\/(\d{4})$/);
    if (match) {
        var year = parseInt(match[3], 10);
        var month = parseInt(match[2], 10) - 1; // months are 0-11
        var day = parseInt(match[1], 10);
        var date = new Date(year, month, day);
        if (date.getFullYear() !== year || date.getMonth() != month || date.getDate() !== day) {
            this.value = '';
        }
    } else {
    }
});

var input = document.getElementById('NASCIMENTO');

input.addEventListener('keydown', function (e) {
    // Verifique se a tecla pressionada foi a tecla 'Delete'
    if (e.key === 'Delete') {
        // Se foi, limpe o campo de entrada
        this.value = '';
    }
});


input.addEventListener('blur', function (e) {
    this.type = 'text';
    var match = this.value.match(/^(\d{2})\/(\d{2})\/(\d{4})$/);
    if (match) {
        var year = parseInt(match[3], 10);
        var month = parseInt(match[2], 10) - 1; // months are 0-11
        var day = parseInt(match[1], 10);
        var date = new Date(year, month, day);
        if (date.getFullYear() !== year || date.getMonth() != month || date.getDate() !== day) {
            showErrorModal('Data inválida');
            this.value = '';
        }
    } else {
        showErrorModal('Formato inválido. Por favor, insira a data no formato DD/MM/AAAA');
        this.value = '';
    }
});

function showErrorModal(message) {
    $('#modalBody').text(message);
    $('#myModal').modal('show');
}
