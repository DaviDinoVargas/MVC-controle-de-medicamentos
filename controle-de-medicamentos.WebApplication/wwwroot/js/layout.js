function toggleSidebar() {
    const sidebar = document.getElementById('sidebar');
    const icon = document.getElementById('toggle-icon');

    sidebar.classList.toggle('collapsed');

    icon.textContent = sidebar.classList.contains('collapsed')
        ? 'keyboard_double_arrow_right'
        : 'keyboard_double_arrow_left';

    if (sidebar.classList.contains('collapsed')) {
        document.querySelectorAll('.menu-item.has-submenu').forEach(item => {
            item.classList.remove('open');
            const submenuIcon = item.querySelector('.submenu-icon');
            if (submenuIcon) submenuIcon.textContent = 'keyboard_arrow_down';
        });
    }
}
function toggleSubmenu(button) {
    const item = button.closest('.menu-item');
    const icon = button.querySelector('.submenu-icon');

    item.classList.toggle('open');
    icon.textContent = item.classList.contains('open')
        ? 'keyboard_arrow_up'
        : 'keyboard_arrow_down';
}
const breadcrumb = document.getElementById('breadcrumb');

function updateBreadcrumb(...items) {
    breadcrumb.innerHTML = '';
    items.forEach((text, index) => {
        const span = document.createElement('span');
        span.className = 'breadcrumb-item';
        span.textContent = text;
        breadcrumb.appendChild(span);
    });
}

document.querySelectorAll('.menu li a').forEach(link => {
    link.addEventListener('click', e => {
        const mainText = link.closest('.has-submenu')?.querySelector('.menu-text')?.textContent.trim();
        const subText = link.textContent.trim();

        if (mainText) updateBreadcrumb(mainText, subText);
        else updateBreadcrumb(subText);
    });
});

document.querySelector('.menu li.active a').addEventListener('click', () => {
    updateBreadcrumb('Início');
});

function toggleSubmenu(button) {
    const sidebar = document.getElementById('sidebar');
    const item = button.closest('.menu-item');
    const icon = button.querySelector('.submenu-icon');

    if (sidebar.classList.contains('collapsed')) {
        sidebar.classList.remove('collapsed');
        document.getElementById('toggle-icon').textContent = 'keyboard_double_arrow_left';
    }

    const isOpen = item.classList.toggle('open');
    icon.textContent = isOpen ? 'keyboard_arrow_up' : 'keyboard_arrow_down';
}

