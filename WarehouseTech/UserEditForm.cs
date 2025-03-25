using WarehouseTech.Models;
using System;
using System.Windows.Forms;
using WarehouseTech.Repositories.Interfaces;
using System.Threading.Tasks;

namespace WarehouseTech
{
    public partial class UserEditForm : StyledForm
    {
        public User User { get; private set; }
        public string Password { get; private set; }

        private readonly bool _isNewUser;
        private readonly IUserRepository _userRepository;

        public UserEditForm(IUserRepository userRepository, User user = null, bool isNewUser = false)
        {
            InitializeComponent();
            _userRepository = userRepository;
            _isNewUser = isNewUser;
            User = user ?? new User();
            InitializeData();
        }
        private async Task LoadRolesAsync()
        {
            try
            {
                var roles = await _userRepository.GetAllRolesAsync();
                cmbRole.DataSource = roles;
                cmbRole.DisplayMember = "Name";
                cmbRole.ValueMember = "Id";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка загрузки ролей: {ex.Message}");
            }
        }
        private async void InitializeData()
        {
            await LoadRolesAsync(); // Загружаем роли

            txtUsername.Text = User.Username;
            cmbRole.SelectedValue = User.Role_id;

            if (_isNewUser)
            {
                txtPassword.Enabled = true;
                txtConfirm.Enabled = true;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateForm())
            {
                User.Username = txtUsername.Text;
                User.Role_id = (int)cmbRole.SelectedValue;
                Password = txtPassword.Text;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private bool ValidateForm()
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text))
            {
                MessageBox.Show("Введите логин пользователя");
                return false;
            }

            if (_isNewUser && string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Введите пароль");
                return false;
            }

            if (txtPassword.Text != txtConfirm.Text)
            {
                MessageBox.Show("Пароли не совпадают");
                return false;
            }

            return true;
        }
    }
}