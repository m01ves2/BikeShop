window.authStorage = {

    saveToken: function (token) {
        localStorage.setItem("jwt", token);
    },

    getToken: function () {
        return localStorage.getItem("jwt");
    },

    removeToken: function () {
        localStorage.removeItem("jwt");
    }

};