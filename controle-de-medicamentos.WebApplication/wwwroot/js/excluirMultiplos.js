// excluir multiplos:
console.log("Script acaoTabelas.js carregado!"); // Verifique se aparece no console

document.addEventListener('DOMContentLoaded', function () {
    console.log("DOM totalmente carregado");

    const btnDeleteMultiplos = document.querySelector('.btn-delete');
    console.log("Elemento btn-delete encontrado:", btnDeleteMultiplos);

    if (!btnDeleteMultiplos) {
        console.error("Botão delete não encontrado! Verifique a classe no HTML");
        return;
    }

    btnDeleteMultiplos.addEventListener('click', async function (e) {
        console.log("Botão delete clicado!");
        e.preventDefault();

        // Verificação mais robusta dos checkboxes
        const checkboxes = document.querySelectorAll('input[type="checkbox"].checkbox:checked');
        console.log(`Checkboxes selecionados: ${checkboxes.length}`);

        if (checkboxes.length === 0) {
            console.warn("Nenhum checkbox selecionado");
            alert('Por favor, selecione pelo menos um medicamento para excluir.');
            return;
        }

        const ids = [];
        checkboxes.forEach(checkbox => {
            const id = parseInt(checkbox.value);
            if (isNaN(id)) {
                console.error(`Valor inválido no checkbox: ${checkbox.value}`);
            } else {
                ids.push(id);
            }
        });
        console.log("IDs para exclusão:", ids);

        if (!confirm(`Tem certeza que deseja excluir os ${ids.length} medicamentos selecionados?`)) {
            console.log("Usuário cancelou a exclusão");
            return;
        }

        try {
            console.log("Iniciando requisição para /medicamentos/excluirMultiplos...");

            const response = await fetch('/medicamentos/excluirMultiplos', {
                method: 'POST',
                headers: {
                    'Content-Type': 'application/json',
                },
                body: JSON.stringify(ids)
            });
            console.log("Resposta recebida. Status:", response.status);

            const result = await response.json();
            console.log("Resultado completo:", result);

            if (result.success) {
                alert(result.message);
                console.log("Recarregando a página...");
                window.location.reload();
            } else {
                alert(result.message);
            }
        } catch (error) {
            console.error("Erro completo:", error);
            alert('Falha ao excluir medicamentos. Verifique o console (F12) para detalhes.');
        }
    });

    // Verificação do selectAll
    const selectAll = document.getElementById('selectAll');
    if (selectAll) {
        selectAll.addEventListener('change', function () {
            console.log("Select all alterado:", this.checked);
            const checkboxes = document.querySelectorAll('.checkbox');
            checkboxes.forEach(checkbox => {
                checkbox.checked = this.checked;
            });
        });
    } else {
        console.warn("Elemento selectAll não encontrado");
    }
});