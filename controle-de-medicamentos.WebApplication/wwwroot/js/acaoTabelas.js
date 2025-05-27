
document.addEventListener('DOMContentLoaded', () => {
    document.querySelectorAll('.menu-btn').forEach(button => {
        button.addEventListener('click', function (event) {
            event.stopPropagation(); 

            document.querySelectorAll('.menu-dropdown').forEach(menu => {
                if (menu !== this.nextElementSibling) {
                    menu.style.display = 'none';
                }
            });
            const dropdown = this.nextElementSibling;
            if (dropdown.style.display === 'block') {
                dropdown.style.display = 'none';
                this.setAttribute('aria-expanded', 'false');
            } else {
                dropdown.style.display = 'block';
                this.setAttribute('aria-expanded', 'true');
            }
        });
    });

    document.addEventListener('click', () => {
        document.querySelectorAll('.menu-dropdown').forEach(menu => {
            menu.style.display = 'none';
        });
        document.querySelectorAll('.menu-btn').forEach(btn => {
            btn.setAttribute('aria-expanded', 'false');
        });
    });
});
