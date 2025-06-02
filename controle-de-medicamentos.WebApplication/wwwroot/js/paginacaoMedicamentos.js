document.addEventListener("DOMContentLoaded", () => {
    const itensPorPaginaButtons = document.querySelectorAll(".itens-por-pagina-retangulo button");
    const paginacaoContainer = document.querySelector(".paginacao-paginas");
    const tabelaBody = document.querySelector(".medicamentos-table tbody");
    const exibicaoInfo = document.querySelector(".paginacao-end span");

    let itensPorPagina = 25;
    let paginaAtual = 1;

    function renderTabela() {
        const inicio = (paginaAtual - 1) * itensPorPagina;
        const fim = inicio + itensPorPagina;
        const registros = medicamentos.slice(inicio, fim);

        exibicaoInfo.textContent = `Exibindo ${inicio + 1}–${Math.min(fim, medicamentos.length)} de ${medicamentos.length}`;

        tabelaBody.innerHTML = "";

        registros.forEach(m => {
            const tr = document.createElement("tr");
            tr.className = m.Quantidade < 20 ? "status-falta" : "status-disponivel";
            tr.tabIndex = 0;
            tr.innerHTML = `
                <td><input type="checkbox" class="checkbox" name="selecionados" value="${m.Id}" /></td>
                <td>${m.Id}</td>
                <td>${m.Nome}</td>
                <td>${m.NomeFornecedor || "Sem fornecedor"}</td>
                <td><span class="${m.Quantidade < 20 ? "status-alerta" : "status-ok"}">
                    ${m.Quantidade < 20 ? "EM FALTA" : "Disponível"}
                </span></td>
                <td class="acoes-coluna">
                    <button class="menu-btn" aria-haspopup="true">
                        <span class="material-icons">more_vert</span>
                    </button>
                    <div class="menu-dropdown" role="menu">
                        <a href="/medicamentos/editar/${m.Id}" role="menuitem">Editar</a>
                        <a href="/medicamentos/excluir/${m.Id}" role="menuitem">Excluir</a>
                    </div>
                </td>
            `;
            tabelaBody.appendChild(tr);
        });

        renderPaginacao();
    }

    function renderPaginacao() {
        const totalPaginas = Math.ceil(medicamentos.length / itensPorPagina);
        paginacaoContainer.innerHTML = "";

        for (let i = 1; i <= totalPaginas; i++) {
            const btn = document.createElement("button");
            btn.className = "pagina" + (i === paginaAtual ? " ativa" : "");
            btn.textContent = i;
            btn.addEventListener("click", () => {
                paginaAtual = i;
                renderTabela();
            });
            paginacaoContainer.appendChild(btn);
        }
    }

    itensPorPaginaButtons.forEach(btn => {
        btn.addEventListener("click", () => {

            itensPorPaginaButtons.forEach(b => b.classList.remove("ativo"));

            btn.classList.add("ativo");

            itensPorPagina = parseInt(btn.dataset.valor);

            paginaAtual = 1;

            renderTabela();
        });
    });


    renderTabela();
});