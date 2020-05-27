namespace Books.Index {
    export class Index {
        public BooksSet: any = $("#booksJSON").val();
        constructor () {
            this.addEvents();
        }

        private addEvents(): void {
            this.addToCartEvent(1);
        }

        private addToCartEvent(id: number): void {

        }


    }
}

