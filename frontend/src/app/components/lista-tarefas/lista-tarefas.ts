import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { TarefaService } from '../../services/tarefa';
import { Tarefa } from '../../models/tarefa.model';

@Component({
  selector: 'app-lista-tarefas',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './lista-tarefas.html',
  styleUrl: './lista-tarefas.css'
})
export class ListaTarefasComponent implements OnInit {
  tarefas: Tarefa[] = [];
  
  // Objeto que espelha os campos do formulário para criar/editar
  novaTarefa: Tarefa = { titulo: '', descricao: '', status: 'Pendente' };
  
  // Controle para saber se estamos editando uma tarefa existente
  editando: boolean = false;

  constructor(private tarefaService: TarefaService) {}

  ngOnInit(): void {
    this.carregarTarefas();
  }

  // 1. LISTAR (GET)
  carregarTarefas(): void {
    this.tarefaService.getTarefas().subscribe({
      next: (dados) => this.tarefas = dados,
      error: (erro) => console.error('Erro ao buscar tarefas:', erro)
    });
  }

  // 2. CRIAR OU ATUALIZAR (POST / PUT)
  salvarTarefa(): void {
    if (!this.novaTarefa.titulo.trim()) return;

    if (this.editando && this.novaTarefa.id) {
      // Se está editando, chama o PUT
      this.tarefaService.atualizarTarefa(this.novaTarefa.id, this.novaTarefa).subscribe({
        next: () => {
          this.carregarTarefas();
          this.resetarFormulario();
        },
        error: (erro) => console.error('Erro ao atualizar tarefa:', erro)
      });
    } else {
      // Se é nova, chama o POST
      this.tarefaService.criarTarefa(this.novaTarefa).subscribe({
        next: () => {
          this.carregarTarefas();
          this.resetarFormulario();
        },
        error: (erro) => console.error('Erro ao criar tarefa:', erro)
      });
    }
  }

  // Prepara a tela para o modo de edição
  prepararEdicao(tarefa: Tarefa): void {
    this.novaTarefa = { ...tarefa }; // Copia os dados para o formulário
    this.editando = true;
  }

  // 3. EXCLUIR (DELETE)
  excluirTarefa(id: number | undefined): void {
    if (!id) return;
    
    if (confirm('Tem certeza que deseja excluir esta tarefa?')) {
      this.tarefaService.excluirTarefa(id).subscribe({
        next: () => this.carregarTarefas(),
        error: (erro) => console.error('Erro ao excluir tarefa:', erro)
      });
    }
  }

  resetarFormulario(): void {
    this.novaTarefa = { titulo: '', descricao: '', status: 'Pendente' };
    this.editando = false;
  }
}