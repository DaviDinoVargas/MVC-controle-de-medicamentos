
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

function updateBreadcrumb(...items) {
    const breadcrumb = document.getElementById('breadcrumb');
    if (!breadcrumb) return;

    breadcrumb.innerHTML = '';
    items.forEach((text, index) => {
        const span = document.createElement('span');
        span.className = 'breadcrumb-item';
        span.textContent = text;
        breadcrumb.appendChild(span);
    });
}
function activateCurrentMenuItem() {
    const currentPath = window.location.pathname;
    console.log('Current path:', currentPath);

    document.querySelectorAll('.menu li, .submenu a').forEach(el => {
        el.classList.remove('active', 'current');
    });

    const routeConfig = {
        '/': {
            selector: '.menu li:nth-child(2) a',
            breadcrumb: ['Início']
        },
        '/medicamentos/visualizar': {
            parentId: 'visualizacoes-menu',
            selector: '#medicamentos-visualizar',
            breadcrumb: ['Visualizações', 'Medicamentos']
        },
        '/fornecedores/visualizar': {
            parentId: 'visualizacoes-menu',
            selector: '#fornecedor-visualizar',
            breadcrumb: ['Visualizações', 'Fornecedor']
        },
        '/medicamentos/cadastrar': {
            parentId: 'cadastros-menu',
            selector: '#medicamentos-cadastrar',
            breadcrumb: ['Cadastros', 'Medicamentos']
        },
        '/fornecedores/cadastrar': {
            parentId: 'cadastros-menu',
            selector: '#fornecedor-cadastrar',
            breadcrumb: ['Cadastros', 'Fornecedor']
        }
    };

    const matchedRoute = Object.keys(routeConfig).find(route =>
        currentPath.toLowerCase() === route.toLowerCase()
    ) || '/';

    const config = routeConfig[matchedRoute];

    if (config.selector) {
        const element = document.querySelector(config.selector);
        if (element) {
            element.classList.add('current');


            if (config.parentId) {
                const parentMenu = document.getElementById(config.parentId);
                if (parentMenu) {
                    parentMenu.classList.add('open');
                    parentMenu.querySelector('.menu-link').classList.add('active');
                }
            }
        }
    }

    updateBreadcrumb(...config.breadcrumb);
}

document.addEventListener('DOMContentLoaded', function () {
    activateCurrentMenuItem();


    document.querySelectorAll('.menu a').forEach(link => {
        link.addEventListener('click', function (e) {

            if (this.classList.contains('menu-link')) return;


            setTimeout(activateCurrentMenuItem, 50);
        });
    });
});

window.addEventListener('load', activateCurrentMenuItem);