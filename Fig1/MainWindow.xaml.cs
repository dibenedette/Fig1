using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Fig1
{
    /// <summary>
    /// Interação lógica para MainWindow.xam
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private string operacao;
        DatePicker dpdata = new DatePicker();
        private void BtSalvar_Click(object sender, RoutedEventArgs e)
        {
                        
            if (operacao == "inserir")
            {
                //paciente com os dados na tela
                paciente p = new paciente();
                p.paciente1 = txtPaciente.Text;
                p.especie = txtEspecie.Text;
                p.raca = txtRaca.Text;
                p.sexo = txtSexo.Text;
                p.idade = Convert.ToInt32(txtIdade.Text);
                p.nometutor = txtNomeTutor.Text;
                p.emailtutor = txtEmailTutor.Text;
                p.telefonetutor = txtTelTutor.Text;
                p.nomeveterinario = txtNomeVet.Text;
                p.emailveterinario = txtEmailvet.Text;
                p.data = dpdata.SelectedDate;

                //gravar no banco de dados
                using (CadastroEntities ctx = new CadastroEntities())
                {

                    ctx.pacientes.Add(p);
                    ctx.SaveChanges();
                }
            }
            if (operacao == "alterar");
            {
                using (CadastroEntities ctx = new CadastroEntities())
                {
                    
                   
                   
                    paciente p = ctx.pacientes.Find(Convert.ToInt32(txtId.Text));
                    

                    //paciente c = new paciente();

                    if (p != null)
                    {
                        p.paciente1 = txtPaciente.Text;
                        p.especie = txtEspecie.Text;
                        p.raca = txtRaca.Text;
                        p.sexo = txtSexo.Text;
                        p.idade = Convert.ToInt32(txtIdade.Text);
                        p.nometutor = txtNomeTutor.Text;
                        p.emailtutor = txtEmailTutor.Text;
                        p.telefonetutor = txtTelTutor.Text;
                        p.nomeveterinario = txtNomeVet.Text;
                        p.emailveterinario = txtEmailvet.Text;
                        p.data = dpdata.SelectedDate;

                        ctx.SaveChanges();
                    }
                }
            }

            this.ListarPacientes();
            this.AlteraBotoes(1);
            this.LimpaCampos();
        }

        private void LimpaCampos()
        {

            txtId.IsEnabled = true;
            txtId.Clear();
            txtPaciente.Clear();
            txtRaca.Clear();
            txtSexo.Clear();
            txtEspecie.Clear();
            txtIdade.Clear();
            txtNomeTutor.Clear();
            txtEmailTutor.Clear();
            txtTelTutor.Clear();
            txtNomeVet.Clear();
            txtEmailvet.Clear();
            txtTelVet.Clear();
            dpdata.SelectedDate = null;
            
            
            
            
        }

        private void BtInserir_Click(object sender, RoutedEventArgs e)
        {
            this.operacao = "inserir";
            this.AlteraBotoes(2);
            this.LimpaCampos();
            txtId.IsEnabled = false;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.ListarPacientes();
            this.AlteraBotoes(1);
        }

        private void ListarPacientes()

        {
            ////var ctx = new CadastroEntities();
            ////int a = ctx.pacientes.Count();
            ////lbQtdPacientes.Content = "Pacientes: " + a.ToString();
            // var consulta = from p in ctx.pacientes select new { p.paciente1, p.nometutor, p.nomeveterinario };
            // dgGrid.ItemsSource = consulta.ToList();
            //}
            // {
            using (CadastroEntities ctx = new CadastroEntities())
            {
                int a = ctx.pacientes.Count();
                lbQtdPacientes.Content = a.ToString();
                var consulta = ctx.pacientes;
                dgGrid.ItemsSource = consulta.ToList();

            }
         }

        private void AlteraBotoes(int op)
        {
            btAlterar.IsEnabled = false;
            btInserir.IsEnabled = false;
            btExcluir.IsEnabled = false;
            btCancelar.IsEnabled = false;
            btLocalizar.IsEnabled = false;
            btSalvar.IsEnabled = false;
            if (op == 1)
            {//ativar as opções iniciais
                btInserir.IsEnabled = true;
                btLocalizar.IsEnabled = true;
            }
            if (op == 2)
            {
                btCancelar.IsEnabled = true;
                btSalvar.IsEnabled = true;
            }
            if (op == 3)
            {
                btAlterar.IsEnabled = true;
                btExcluir.IsEnabled = true;
            }
        }

        private void BtCancelar_Click(object sender, RoutedEventArgs e)
        {
            this.AlteraBotoes(1);
            this.LimpaCampos();
        }

        private void BtLocalizar_Click(object sender, RoutedEventArgs e)
        {
            if (txtId.Text.Trim().Count() > 0)
            {
                //buscar pelo código
                try
                {
                    int id = Convert.ToInt32(txtId.Text);
                    using (CadastroEntities ctx = new CadastroEntities())
                    {
                        paciente p = ctx.pacientes.Find(id);
                        dgGrid.ItemsSource = new paciente[1] { p };
                    }
                }
                catch { }
            }
            // localizar por animal
            if (txtPaciente.Text.Trim().Count() > 0)
            {
                try
                {
                    using (CadastroEntities ctx = new CadastroEntities())
                    {
                        var consulta = from p in ctx.pacientes
                                       where p.paciente1.Contains(txtPaciente.Text)
                                       select p;
                        dgGrid.ItemsSource = consulta.ToList();
                    }
                }
                catch { }
            }
            // localizar por tutor
            if (txtNomeTutor.Text.Trim().Count() > 0)
            {
                try
                {
                    using (CadastroEntities ctx = new CadastroEntities())
                    {
                        var consulta = from p in ctx.pacientes
                                       where p.nometutor.Contains(txtNomeTutor.Text)
                                       select p;
                        dgGrid.ItemsSource = consulta.ToList();
                    }
                }
                catch { }
            }
            // localizar por Veterinário
            if (txtNomeVet.Text.Trim().Count() > 0)
            {
                try
                {
                    using (CadastroEntities ctx = new CadastroEntities())
                    {
                        var consulta = from p in ctx.pacientes
                                       where p.nomeveterinario.Contains(txtNomeVet.Text)
                                       select p;
                        dgGrid.ItemsSource = consulta.ToList();
                    }
                }
                catch { }
            }
        }

        private void DgGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (dgGrid.SelectedIndex >= 0)
            {
                paciente p = (paciente)dgGrid.SelectedItem;
                txtPaciente.Text = p.paciente1;
                txtEspecie.Text = p.especie;
                txtId.Text = p.Id.ToString();
                txtRaca.Text = p.raca;
                txtIdade.Text = p.idade.ToString();
                txtSexo.Text = p.sexo;
                txtNomeTutor.Text = p.nometutor;
                txtEmailTutor.Text = p.emailtutor;
                txtTelTutor.Text = p.telefonetutor;
                txtNomeVet.Text = p.nomeveterinario;
                txtEmailvet.Text = p.emailveterinario;
                txtTelVet.Text = p.telefoneveterinario;

                this.AlteraBotoes(3);
            }
        }

        private void BtAlterar_Click(object sender, RoutedEventArgs e)
        {
            this.operacao = "alterar";
            this.AlteraBotoes(2);
            //this.LimpaCampos();
            txtId.IsEnabled = true;
        }

        private void BtExcluir_Click(object sender, RoutedEventArgs e)
        {
            using (CadastroEntities ctx = new CadastroEntities())
            {
                paciente p = ctx.pacientes.Find(Convert.ToInt32(txtId.Text));
                if (p != null)
                {
                    ctx.pacientes.Remove(p);
                    ctx.SaveChanges();
                }
                p.paciente1 = txtPaciente.Text;
                p.especie = txtEspecie.Text;
                p.raca = txtRaca.Text;
                p.sexo = txtSexo.Text;
                p.idade = Convert.ToInt32(txtIdade.Text);
                p.nometutor = txtNomeTutor.Text;
                p.emailtutor = txtEmailTutor.Text;
                p.telefonetutor = txtTelTutor.Text;
                p.nomeveterinario = txtNomeVet.Text;
                p.emailveterinario = txtEmailvet.Text;
                p.data = dpdata.SelectedDate;
            }
        }
    }
}
