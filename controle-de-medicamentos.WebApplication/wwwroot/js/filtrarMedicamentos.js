const searchInput = document.getElementById("searchInput");
const sortFilter = document.getElementById("sortFilter");
const tabelaMedicamentos = document.querySelector(".medicamentos-table tbody");

function renderTabela(dados) {
    if (dados.length === 0) {
        tabelaMedicamentos.innerHTML = `<tr><td colspan="6">Nenhum medicamento encontrado.</td></tr>`;
        return;
    }

    let html = "";

    dados.forEach(m => {
        const statusTexto = m.Quantidade < 20 ? "Em falta" : "Disponível";
        const statusClasse = m.Quantidade < 20 ? "status-alerta" : "status-ok";
        const linhaClasse = m.Quantidade < 20 ? "status-falta" : "status-disponivel";

        html += `
            <tr class="${linhaClasse}" tabindex="0">
                <td><input type="checkbox" class="checkbox" name="selecionados" value="${m.Id}" /></td>
                <td>${m.Id}</td>
                <td>${m.Nome}</td>
                <td>${m.NomeFornecedor || "Sem fornecedor"}</td>
                <td><span class="${statusClasse}">${statusTexto}</span></td>
                <td class="acoes-coluna">
                    <button class="menu-btn" aria-haspopup="true" aria-expanded="false" aria-label="Menu de ações">
                        <span class="material-icons">more_vert</span>
                    </button>
                    <div class="menu-dropdown" role="menu">
                        <a href="/medicamentos/editar/${m.Id}" role="menuitem">Editar</a>
                        <a href="/medicamentos/excluir/${m.Id}" role="menuitem">Excluir</a>
                    </div>
                </td>
            </tr>`;
    });

    tabelaMedicamentos.innerHTML = html;
}

function normalizarTexto(texto) {
    return texto.normalize("NFD").replace(/[\u0300-\u036f]/g, "").toLowerCase();
}

function filtrarEOrdenar() {
    const textoFiltro = normalizarTexto(searchInput.value.trim());

    let filtrados = medicamentos.filter(m => {
        const nome = normalizarTexto(m.Nome || "");
        const fornecedor = normalizarTexto(m.NomeFornecedor || "");
        const status = m.Quantidade < 20 ? "em falta" : "disponivel";
        return nome.includes(textoFiltro) ||
            fornecedor.includes(textoFiltro) ||
            status.includes(textoFiltro);
    });

    switch (sortFilter.value) {
        case "valorMaior":
            filtrados.sort((a, b) => (b.Valor || 0) - (a.Valor || 0));
            break;
        case "valorMenor":
            filtrados.sort((a, b) => (a.Valor || 0) - (b.Valor || 0));
            break;
        case "recent":
            filtrados.sort((a, b) => b.Id - a.Id);
            break;
        case "oldest":
            filtrados.sort((a, b) => a.Id - b.Id);
            break;
    }

    renderTabela(filtrados);
}

searchInput.addEventListener("input", filtrarEOrdenar);
sortFilter.addEventListener("change", filtrarEOrdenar);

renderTabela(medicamentos);
