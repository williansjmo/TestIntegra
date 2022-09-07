function Success(title) {
    Swal.fire({
        position: 'top-center',
        icon: 'success',
        title: title,
        showConfirmButton: false,
        timer: 1500
    });
}
function Errors(title) {
    Swal.fire({
        position: 'top-center',
        icon: 'error',
        title: title,
        showConfirmButton: false,
        timer: 1500
    });
}