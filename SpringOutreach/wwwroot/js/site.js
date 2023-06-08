// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

// Initialize datatables
$(document).ready(function () {
    $('#mainTable').DataTable({
    "language": {
        "url": "//cdn.datatables.net/plug-ins/9dcbecd42ad/i18n/English.json"
        },
        columnDefs: [
            {
                orderable: false, targets: "no-sort"
            }],
    "paging": false, "info": false, "search": true, "ordering": true, 
    });
});
