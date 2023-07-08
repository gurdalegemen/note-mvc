// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

const { data } = require("jquery");

// Write your JavaScript code.

function addNote() {
    const form = document.getElementById("noteInfo")
    if (form.style.display === 'none')
    {
        form.style.display = 'flex';
    }
    else
    {
        form.style.display = 'none';

    }
}

function submitForm(event,id) {
    event.preventDefault(); // Prevent the default form submission

    var title = document.getElementById("editedTitle-"+id).innerHTML;
    var content = document.getElementById("editedContent-"+id).innerHTML;

    // Make the PUT request using AJAX
    $.ajax({
        url: "/Note/UpdateNote/"+ id, // Replace with your API endpoint URL
        type: "PUT",
        contentType: "application/json",
        data: JSON.stringify({ Title: title, Content: content }),
        success: function (result) {
            // Handle successful response
            console.log(result);
        },
        error: function (xhr, status, error) {
            // Handle error
            console.error(error);
        }
    });
}


