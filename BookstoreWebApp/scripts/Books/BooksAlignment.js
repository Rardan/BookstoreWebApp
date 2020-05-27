
/// AddToShoppingCart function redirection to ShoppingCart controller
///param @id - id książki
function AddToCart(id) {
    window.location.href = "/ShoppingCart/AddToShoppingCart/" + id;
}

///Section GLOBAL VARIABLES START

/// Get all books from controller
var BooksJson = '@Html.Raw(ViewBag.Books)';
/// Body of the page
var Body = null;
///Main Container
var MainContainer = document.getElementById("whatthehellisgoingon");


function GetMainElement() {
    var MainContainer = document.getElementById("whatthehellisgoingon");
}
///Section GLOBAL VARIABLES END

/// Incomplete Book Class
class Book{
    constructor(Id, Title, Price, Description, ISBN, Photo, AuthorName) {
        this.Id = Id;
        this.Title = Title;
        this.AuthorName = AuthorName;
        this.Price = Price;
        this.Description = Description;
        this.ISBN = ISBN;
        this.Photo = Photo;
    }
    ///Override toString function in order to return card with book
    toString(){
        return "<div class=\"card h-100 m-1\">" +
            "<div class=\"card-header\">" + this.Title + "<br/>" + "Author:" + this.AuthorName + "</div>" +
                + "<a asp-action=\"Details\" asp-controller=\"Books\" asp-route-id=\"" + this.Id + "\"><img class=\"card-img-extdk\" src=\"" + this.Photo + "\"></a>" +
                + "<div class=\"card-footer\">" +
                    "Price:" + this.Price + "<br/>" +
                "<form asp-action=\"Details\" asp-route-id=\"" + this.Id + "method=\"get\">" +
                "<input type=\"button\" class=\"btn cartbutton-responsive\" onclick=\"addToCartEvent(" + this.Id + ") />" +
                "</form></div></div>";
    }
}

///Generate grid with books
function GenerateGrid() {
    console.log("Function GenerateGrid starts");
    var BooksArray = [];
    for (var jsonitem in BooksJson) {
        var parsedObject = JSON.parse(jsonitem);
        var book = new Book(parsedObject.Id, parsedObject.Title, parsedObject.Price, parsedObject.Description, parsedObject.ISBN, parsedObject.Photo, parsedObject.AuthorId);
        BooksArray.push(book);
    }
    var RowCounter = 0;
    Body += "<div class=\"row m-1 justify-content-center\">";
    for (var book in BooksArray) {
        if (RowCounter % 5 === 0) {
            Body += "</div><div class=\"row m-1 justify-content-center\">";
        }
        Body += "<div class=\"col-xs-12 col-sm-6 col-md-4 col-lg-3 m-2\">" + book.toString() + "</div>";
        RowCounter++;
    }
}

///Main Content generation
function GenerateContent() {
    console.log("Function GenerateContent start");
    GenerateGrid();
    alert(MainContainer);
    console.log(Body);
    MainContainer.innerHTML += Body;
}

