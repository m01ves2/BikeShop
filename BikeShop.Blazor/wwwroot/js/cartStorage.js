window.cartStorage = {

    get: function () {
        return localStorage.getItem("cart");
    },

    set: function (value) {
        localStorage.setItem("cart", value);
    },

    clear: function () {
        localStorage.removeItem("cart");
    }
};