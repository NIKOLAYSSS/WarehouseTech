using WarehouseTech.Models;
using WarehouseTech.Services;
using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using WarehouseTech.Repositories.Interfaces;
using WarehouseTech.Services.Interfaces;

namespace WarehouseTech
{
    public partial class UsersForm : StyledForm
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        private BindingSource _usersBinding = new BindingSource();

        public UsersForm(IAuthService authService, IUserRepository userRepository)
        {
            InitializeComponent();
            _authService = authService;
            _userRepository = userRepository;
            ConfigureGrid();
            LoadRoles();
            LoadUsersAsync();
            cmbRole.SelectedIndexChanged += cmbRole_SelectedIndexChanged;
        }

        private void ConfigureGrid()
        {
            dgvUsers.AutoGenerateColumns = false;
            dgvUsers.Columns.Clear();

            dgvUsers.Columns.AddRange(
                new DataGridViewTextBoxColumn { DataPropertyName = "Id", HeaderText = "ID", Width = 50 },
                new DataGridViewTextBoxColumn { DataPropertyName = "Username", HeaderText = "Логин", Width = 150 },
                new DataGridViewTextBoxColumn { DataPropertyName = "RoleName", HeaderText = "Роль" },
                new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "CreatedAt",
                    HeaderText = "Дата создания",
                    Width = 120,
                    DefaultCellStyle = new DataGridViewCellStyle { Format = "dd.MM.yyyy" }
                },
                new DataGridViewTextBoxColumn
                {
                    DataPropertyName = "LastLogin",
                    HeaderText = "Последний вход",
                    Width = 120,
                    DefaultCellStyle = new DataGridViewCellStyle
                    {
                        Format = "dd.MM.yyyy HH:mm",
                        NullValue = "Не входил"  // Обработка NULL
                    }
                }
            );

            dgvUsers.DataSource = _usersBinding;
        }

        private async void LoadRoles()
        {
            var roles = await _userRepository.GetAllRolesAsync();

            // Добавляем пункт "Все роли"
            var allRoles = new List<Role> { new Role { Id = 0, Name = "Все роли" } };
            allRoles.AddRange(roles);

            cmbRole.DataSource = allRoles;
            cmbRole.DisplayMember = "Name";
            cmbRole.ValueMember = "Id";
        }

        public async Task LoadUsersAsync(int? roleId = null)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                IEnumerable<User> users;

                if (roleId.HasValue && roleId > 0)
                {
                    // Фильтрация по роли
                    users = await _userRepository.GetByRoleAsync(roleId.Value);
                }
                else
                {
                    // Все пользователи
                    users = await _userRepository.GetAllWithRolesAsync();
                }

                // Логирование
                foreach (var user in users)
                {
                    Console.WriteLine($"[DEBUG] User: {user.Username}, Role: {user.Role?.Name ?? "N/A"}");
                }

                _usersBinding.DataSource = users.ToList();
                dgvUsers.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки: {ex.Message}");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private async void btnAdd_Click(object sender, EventArgs e)
        {
            using (var editForm = new UserEditForm(_userRepository, isNewUser: true))
            {
                if (editForm.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        var newUser = editForm.User;
                        newUser.Password_hash = AuthService.HashPassword(editForm.Password);
                        await _userRepository.AddAsync(newUser);
                        await LoadUsersAsync();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка создания: {ex.Message}");
                    }
                }
            }
        }

        private async void btnEdit_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow?.DataBoundItem is User selectedUser)
            {
                using (var editForm = new UserEditForm(_userRepository, selectedUser))
                {
                    if (editForm.ShowDialog() == DialogResult.OK)
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(editForm.Password))
                                selectedUser.Password_hash = AuthService.HashPassword(editForm.Password);

                            await _userRepository.UpdateAsync(selectedUser);
                            await LoadUsersAsync();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Ошибка обновления: {ex.Message}");
                        }
                    }
                }
            }
        }

        private async void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvUsers.CurrentRow?.DataBoundItem is User user)
            {
                if (user.Id == Program.CurrentUser.Id)
                {
                    MessageBox.Show("Нельзя удалить самого себя");
                    return;
                }

                if (MessageBox.Show("Удалить выбранного пользователя?",
                    "Подтверждение",
                    MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    try
                    {
                        await _userRepository.DeleteAsync(user.Id);
                        await LoadUsersAsync();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Ошибка удаления: {ex.Message}");
                    }
                }
            }
        }
        private async void cmbRole_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedRole = cmbRole.SelectedItem as Role;
            int? roleId = selectedRole?.Id;

            // Если выбрана "Все роли" (добавьте этот пункт в ComboBox)
            if (cmbRole.SelectedIndex == 0) roleId = null;

            await LoadUsersAsync(roleId);
        }
        private async void UsersForm_Load(object sender, EventArgs e)
        {
            await LoadUsersAsync();
        }
    }
}