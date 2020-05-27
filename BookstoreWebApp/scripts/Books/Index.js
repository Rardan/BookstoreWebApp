var Books;
(function (Books) {
    var Index;
    (function (Index_1) {
        class Index {
            constructor() {
                this.BooksSet = $("#booksJSON").val();
                this.addEvents();
            }
            addEvents() {
                this.addToCartEvent(1);
            }
            addToCartEvent(id) {
            }
        }
        Index_1.Index = Index;
    })(Index = Books.Index || (Books.Index = {}));
})(Books || (Books = {}));
//# sourceMappingURL=Index.js.map