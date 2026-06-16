window.themeManager = {

    setTheme: function (darkMode) {

        if (darkMode) {

            document.documentElement.classList.add('dark');

            localStorage.setItem('theme', 'dark');

        }
        else {

            document.documentElement.classList.remove('dark');

            localStorage.setItem('theme', 'light');

        }

    },

    getTheme: function () {

        const saved = localStorage.getItem('theme');

        return saved === 'dark';

    }

};