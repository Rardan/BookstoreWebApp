// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function AddToCart(id) {
    window.location.href = "/ShoppingCart/AddToShoppingCart/"+id;
    //window.location.href = "@Url.Action("AddToShoppingCart", "ShoppingCart") " + " / " + id";
}