namespace CKCharaDataEditor
{
    partial class ConditionForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConditionForm));
            dataGridView = new DataGridView();
            ConditionId = new DataGridViewTextBoxColumn();
            Value = new DataGridViewTextBoxColumn();
            Infinity = new DataGridViewCheckBoxColumn();
            Duration = new DataGridViewTextBoxColumn();
            Timer = new DataGridViewTextBoxColumn();
            Description = new DataGridViewComboBoxColumn();
            label1 = new Label();
            createBackupButton = new Button();
            overrideConditionsButton = new Button();
            loadBackupButton = new Button();
            openFileDialog = new OpenFileDialog();
            saveFileDialog = new SaveFileDialog();
            aboudConditionIdLinkLabel = new LinkLabel();
            DataGridErrorTextLabel = new Label();
            updateConditionsLabel = new Label();
            deleteSelectedRowButton = new Button();
            addNewRowButton = new Button();
            conditionListLinkLabel = new LinkLabel();
            ((System.ComponentModel.ISupportInitialize)dataGridView).BeginInit();
            SuspendLayout();
            // 
            // dataGridView
            // 
            dataGridView.AllowUserToAddRows = false;
            dataGridViewCellStyle1.BackColor = Color.Cyan;
            dataGridView.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            dataGridView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView.Columns.AddRange(new DataGridViewColumn[] { ConditionId, Value, Infinity, Duration, Timer, Description });
            dataGridView.EditMode = DataGridViewEditMode.EditOnEnter;
            dataGridView.Location = new Point(12, 27);
            dataGridView.Name = "dataGridView";
            dataGridView.RowHeadersVisible = false;
            dataGridView.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView.Size = new Size(744, 389);
            dataGridView.TabIndex = 0;
            dataGridView.CellValidating += dataGridView_CellValidating;
            // 
            // ConditionId
            // 
            ConditionId.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            ConditionId.FillWeight = 105.964462F;
            ConditionId.HeaderText = "Id";
            ConditionId.MaxInputLength = 3;
            ConditionId.Name = "ConditionId";
            ConditionId.ReadOnly = true;
            ConditionId.SortMode = DataGridViewColumnSortMode.NotSortable;
            ConditionId.Width = 23;
            // 
            // Value
            // 
            Value.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Value.FillWeight = 105.964462F;
            Value.HeaderText = "効果量";
            Value.Name = "Value";
            Value.SortMode = DataGridViewColumnSortMode.NotSortable;
            Value.Width = 49;
            // 
            // Infinity
            // 
            Infinity.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Infinity.FillWeight = 76.1421356F;
            Infinity.HeaderText = "永続化";
            Infinity.Name = "Infinity";
            Infinity.SortMode = DataGridViewColumnSortMode.Automatic;
            Infinity.Width = 68;
            // 
            // Duration
            // 
            Duration.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Duration.FillWeight = 105.964462F;
            Duration.HeaderText = "持続時間";
            Duration.Name = "Duration";
            Duration.SortMode = DataGridViewColumnSortMode.NotSortable;
            Duration.Width = 61;
            // 
            // Timer
            // 
            Timer.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            Timer.FillWeight = 105.964462F;
            Timer.HeaderText = "残り時間";
            Timer.Name = "Timer";
            Timer.SortMode = DataGridViewColumnSortMode.NotSortable;
            Timer.Width = 57;
            // 
            // Description
            // 
            Description.HeaderText = "効果内容";
            Description.Name = "Description";
            Description.Resizable = DataGridViewTriState.True;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(407, 15);
            label1.TabIndex = 1;
            label1.Text = "全てのスキル/バフ・デバフ/食事効果/装備効果はConditionListで管理されています。";
            // 
            // createBackupButton
            // 
            createBackupButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            createBackupButton.Location = new Point(761, 59);
            createBackupButton.Name = "createBackupButton";
            createBackupButton.Size = new Size(186, 23);
            createBackupButton.TabIndex = 2;
            createBackupButton.Text = "バックアップを作成";
            createBackupButton.UseVisualStyleBackColor = true;
            createBackupButton.Click += backUpConditionsButton_Click;
            // 
            // overrideConditionsButton
            // 
            overrideConditionsButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            overrideConditionsButton.Location = new Point(761, 323);
            overrideConditionsButton.Name = "overrideConditionsButton";
            overrideConditionsButton.Size = new Size(186, 23);
            overrideConditionsButton.TabIndex = 3;
            overrideConditionsButton.Text = "現在の内容で上書き";
            overrideConditionsButton.UseVisualStyleBackColor = true;
            overrideConditionsButton.Click += overrideConditionsButton_Click;
            // 
            // loadBackupButton
            // 
            loadBackupButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            loadBackupButton.Location = new Point(761, 30);
            loadBackupButton.Name = "loadBackupButton";
            loadBackupButton.Size = new Size(186, 23);
            loadBackupButton.TabIndex = 4;
            loadBackupButton.Text = "バックアップから読み込む";
            loadBackupButton.UseVisualStyleBackColor = true;
            loadBackupButton.Click += loadConditionsButton_Click;
            // 
            // openFileDialog
            // 
            openFileDialog.Title = "開くファイルを選択してください。";
            // 
            // saveFileDialog
            // 
            saveFileDialog.FileName = "conditionBackup";
            saveFileDialog.Filter = "\"jsonファイル\"|*.json";
            saveFileDialog.Title = "保存先を選択してください。";
            // 
            // aboudConditionIdLinkLabel
            // 
            aboudConditionIdLinkLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            aboudConditionIdLinkLabel.AutoSize = true;
            aboudConditionIdLinkLabel.Location = new Point(762, 168);
            aboudConditionIdLinkLabel.Name = "aboudConditionIdLinkLabel";
            aboudConditionIdLinkLabel.Size = new Size(108, 15);
            aboudConditionIdLinkLabel.TabIndex = 5;
            aboudConditionIdLinkLabel.TabStop = true;
            aboudConditionIdLinkLabel.Text = "Condition値について";
            aboudConditionIdLinkLabel.LinkClicked += aboutConditionIdLinkLabel_LinkClicked;
            // 
            // DataGridErrorTextLabel
            // 
            DataGridErrorTextLabel.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            DataGridErrorTextLabel.AutoSize = true;
            DataGridErrorTextLabel.ForeColor = Color.Red;
            DataGridErrorTextLabel.Location = new Point(14, 419);
            DataGridErrorTextLabel.Name = "DataGridErrorTextLabel";
            DataGridErrorTextLabel.Size = new Size(127, 15);
            DataGridErrorTextLabel.TabIndex = 6;
            DataGridErrorTextLabel.Text = "DataGridErrorTextLabel";
            DataGridErrorTextLabel.Visible = false;
            // 
            // updateConditionsLabel
            // 
            updateConditionsLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            updateConditionsLabel.AutoSize = true;
            updateConditionsLabel.Location = new Point(762, 362);
            updateConditionsLabel.Name = "updateConditionsLabel";
            updateConditionsLabel.Size = new Size(129, 15);
            updateConditionsLabel.TabIndex = 7;
            updateConditionsLabel.Text = "updateConditionsLabel";
            updateConditionsLabel.Visible = false;
            // 
            // deleteSelectedRowButton
            // 
            deleteSelectedRowButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            deleteSelectedRowButton.Location = new Point(761, 125);
            deleteSelectedRowButton.Name = "deleteSelectedRowButton";
            deleteSelectedRowButton.Size = new Size(186, 23);
            deleteSelectedRowButton.TabIndex = 8;
            deleteSelectedRowButton.Text = "選択中の行を削除";
            deleteSelectedRowButton.UseVisualStyleBackColor = true;
            deleteSelectedRowButton.Click += deleteSelectedRowButton_Click;
            // 
            // addNewRowButton
            // 
            addNewRowButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            addNewRowButton.Location = new Point(761, 96);
            addNewRowButton.Name = "addNewRowButton";
            addNewRowButton.Size = new Size(186, 23);
            addNewRowButton.TabIndex = 9;
            addNewRowButton.Text = "新規行を追加";
            addNewRowButton.UseVisualStyleBackColor = true;
            addNewRowButton.Click += addNewRowButton_Click;
            // 
            // conditionListLinkLabel
            // 
            conditionListLinkLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            conditionListLinkLabel.AutoSize = true;
            conditionListLinkLabel.Location = new Point(762, 201);
            conditionListLinkLabel.Name = "conditionListLinkLabel";
            conditionListLinkLabel.Size = new Size(103, 15);
            conditionListLinkLabel.TabIndex = 10;
            conditionListLinkLabel.TabStop = true;
            conditionListLinkLabel.Text = "ConditionIdの一覧";
            conditionListLinkLabel.LinkClicked += conditionListLinkLabel_LinkClicked;
            // 
            // ConditionForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(959, 451);
            Controls.Add(conditionListLinkLabel);
            Controls.Add(addNewRowButton);
            Controls.Add(deleteSelectedRowButton);
            Controls.Add(updateConditionsLabel);
            Controls.Add(DataGridErrorTextLabel);
            Controls.Add(aboudConditionIdLinkLabel);
            Controls.Add(loadBackupButton);
            Controls.Add(overrideConditionsButton);
            Controls.Add(createBackupButton);
            Controls.Add(label1);
            Controls.Add(dataGridView);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MinimumSize = new Size(975, 471);
            Name = "ConditionForm";
            Text = "コンディション値";
            FormClosing += ConditionForm_FormClosing;
            ((System.ComponentModel.ISupportInitialize)dataGridView).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dataGridView;
        private Label label1;
        private Button createBackupButton;
        private Button overrideConditionsButton;
        private Button loadBackupButton;
        private OpenFileDialog openFileDialog;
        private SaveFileDialog saveFileDialog;
        private LinkLabel aboudConditionIdLinkLabel;
        private Label DataGridErrorTextLabel;
        private Label updateConditionsLabel;
        private Button deleteSelectedRowButton;
        private Button addNewRowButton;
        private LinkLabel conditionListLinkLabel;
        private DataGridViewTextBoxColumn ConditionId;
        private DataGridViewTextBoxColumn Value;
        private DataGridViewCheckBoxColumn Infinity;
        private DataGridViewTextBoxColumn Duration;
        private DataGridViewTextBoxColumn Timer;
        private DataGridViewComboBoxColumn Description;
    }
}