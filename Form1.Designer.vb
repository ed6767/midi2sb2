<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Me.Button1 = New System.Windows.Forms.Button()
        Me.FileLbl = New System.Windows.Forms.Label()
        Me.DrumCheckBox = New System.Windows.Forms.CheckBox()
        Me.ChannelTable = New System.Windows.Forms.DataGridView()
        Me.ChannelColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MidiVoiceCol = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.MuteCol = New System.Windows.Forms.DataGridViewCheckBoxColumn()
        Me.VoiceCol = New System.Windows.Forms.DataGridViewComboBoxColumn()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.SaveFileDialog1 = New System.Windows.Forms.SaveFileDialog()
        Me.TabControl1 = New System.Windows.Forms.TabControl()
        Me.TabPage1 = New System.Windows.Forms.TabPage()
        Me.TabPage2 = New System.Windows.Forms.TabPage()
        Me.TabPage3 = New System.Windows.Forms.TabPage()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.LinkLabel1 = New System.Windows.Forms.LinkLabel()
        CType(Me.ChannelTable, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.TabControl1.SuspendLayout()
        Me.TabPage1.SuspendLayout()
        Me.TabPage2.SuspendLayout()
        Me.TabPage3.SuspendLayout()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Dock = System.Windows.Forms.DockStyle.Top
        Me.Button1.Location = New System.Drawing.Point(3, 3)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(346, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Open MIDI"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'FileLbl
        '
        Me.FileLbl.AutoSize = True
        Me.FileLbl.Location = New System.Drawing.Point(6, 32)
        Me.FileLbl.Name = "FileLbl"
        Me.FileLbl.Size = New System.Drawing.Size(64, 13)
        Me.FileLbl.TabIndex = 1
        Me.FileLbl.Text = "No file open"
        '
        'DrumCheckBox
        '
        Me.DrumCheckBox.AutoSize = True
        Me.DrumCheckBox.Checked = True
        Me.DrumCheckBox.CheckState = System.Windows.Forms.CheckState.Checked
        Me.DrumCheckBox.Location = New System.Drawing.Point(319, 500)
        Me.DrumCheckBox.Name = "DrumCheckBox"
        Me.DrumCheckBox.Size = New System.Drawing.Size(73, 17)
        Me.DrumCheckBox.TabIndex = 2
        Me.DrumCheckBox.Text = "Drums Pls"
        Me.DrumCheckBox.UseVisualStyleBackColor = True
        Me.DrumCheckBox.Visible = False
        '
        'ChannelTable
        '
        Me.ChannelTable.AllowUserToAddRows = False
        Me.ChannelTable.AllowUserToDeleteRows = False
        Me.ChannelTable.BackgroundColor = System.Drawing.SystemColors.Control
        Me.ChannelTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.ChannelTable.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ChannelColumn, Me.MidiVoiceCol, Me.MuteCol, Me.VoiceCol})
        Me.ChannelTable.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ChannelTable.Location = New System.Drawing.Point(3, 3)
        Me.ChannelTable.Name = "ChannelTable"
        Me.ChannelTable.RowHeadersVisible = False
        Me.ChannelTable.Size = New System.Drawing.Size(346, 142)
        Me.ChannelTable.TabIndex = 3
        '
        'ChannelColumn
        '
        Me.ChannelColumn.HeaderText = "Channel"
        Me.ChannelColumn.Name = "ChannelColumn"
        Me.ChannelColumn.ReadOnly = True
        Me.ChannelColumn.Resizable = System.Windows.Forms.DataGridViewTriState.[False]
        Me.ChannelColumn.Width = 5
        '
        'MidiVoiceCol
        '
        Me.MidiVoiceCol.HeaderText = "MIDI Instrument"
        Me.MidiVoiceCol.Name = "MidiVoiceCol"
        Me.MidiVoiceCol.ReadOnly = True
        Me.MidiVoiceCol.Width = 150
        '
        'MuteCol
        '
        Me.MuteCol.HeaderText = "Mute"
        Me.MuteCol.Name = "MuteCol"
        Me.MuteCol.Width = 37
        '
        'VoiceCol
        '
        Me.VoiceCol.HeaderText = "Output Instrument"
        Me.VoiceCol.Items.AddRange(New Object() {"Piano", "Electric Piano", "Organ", "Guitar", "Electric Guitar", "Bass", "Pizzicato", "Cello", "Trombone", "Clarinet", "Saxaphone", "Flute", "Wooden Flute", "Bassoon", "Choir", "Vibraphone", "Music Box", "Steel Drum", "Marimba", "Synth Lead", "Synth Pad"})
        Me.VoiceCol.Name = "VoiceCol"
        Me.VoiceCol.Width = 150
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(7, 3)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(341, 23)
        Me.Button2.TabIndex = 4
        Me.Button2.Text = "Convert"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'SaveFileDialog1
        '
        Me.SaveFileDialog1.Filter = "Scratch 2 Projects|*.sb2"
        Me.SaveFileDialog1.Title = "Save MIDI"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.TabPage1)
        Me.TabControl1.Controls.Add(Me.TabPage2)
        Me.TabControl1.Controls.Add(Me.TabPage3)
        Me.TabControl1.Location = New System.Drawing.Point(4, 3)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(360, 112)
        Me.TabControl1.TabIndex = 6
        '
        'TabPage1
        '
        Me.TabPage1.Controls.Add(Me.LinkLabel1)
        Me.TabPage1.Controls.Add(Me.Button1)
        Me.TabPage1.Controls.Add(Me.FileLbl)
        Me.TabPage1.Location = New System.Drawing.Point(4, 22)
        Me.TabPage1.Name = "TabPage1"
        Me.TabPage1.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage1.Size = New System.Drawing.Size(352, 86)
        Me.TabPage1.TabIndex = 0
        Me.TabPage1.Text = "Open"
        Me.TabPage1.UseVisualStyleBackColor = True
        '
        'TabPage2
        '
        Me.TabPage2.Controls.Add(Me.ListBox1)
        Me.TabPage2.Controls.Add(Me.ChannelTable)
        Me.TabPage2.Location = New System.Drawing.Point(4, 22)
        Me.TabPage2.Name = "TabPage2"
        Me.TabPage2.Padding = New System.Windows.Forms.Padding(3)
        Me.TabPage2.Size = New System.Drawing.Size(352, 148)
        Me.TabPage2.TabIndex = 1
        Me.TabPage2.Text = "Instruments"
        Me.TabPage2.UseVisualStyleBackColor = True
        '
        'TabPage3
        '
        Me.TabPage3.Controls.Add(Me.Button2)
        Me.TabPage3.Location = New System.Drawing.Point(4, 22)
        Me.TabPage3.Name = "TabPage3"
        Me.TabPage3.Size = New System.Drawing.Size(352, 148)
        Me.TabPage3.TabIndex = 2
        Me.TabPage3.Text = "Convert"
        Me.TabPage3.UseVisualStyleBackColor = True
        '
        'ListBox1
        '
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.Items.AddRange(New Object() {"Piano", "Electric Piano", "Organ", "Guitar", "Electric Guitar", "Bass", "Pizzicato", "Cello", "Trombone", "Clarinet", "Saxaphone", "Flute", "Wooden Flute", "Bassoon", "Choir", "Vibraphone", "Music Box", "Steel Drum", "Marimba", "Synth Lead", "Synth Pad"})
        Me.ListBox1.Location = New System.Drawing.Point(282, 51)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(120, 95)
        Me.ListBox1.TabIndex = 4
        Me.ListBox1.Visible = False
        '
        'LinkLabel1
        '
        Me.LinkLabel1.AutoSize = True
        Me.LinkLabel1.Location = New System.Drawing.Point(6, 55)
        Me.LinkLabel1.Name = "LinkLabel1"
        Me.LinkLabel1.Size = New System.Drawing.Size(77, 13)
        Me.LinkLabel1.TabIndex = 2
        Me.LinkLabel1.TabStop = True
        Me.LinkLabel1.Text = "View Read Me"
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(368, 126)
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.DrumCheckBox)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "Form1"
        Me.Text = "myed's MIDI converter"
        CType(Me.ChannelTable, System.ComponentModel.ISupportInitialize).EndInit()
        Me.TabControl1.ResumeLayout(False)
        Me.TabPage1.ResumeLayout(False)
        Me.TabPage1.PerformLayout()
        Me.TabPage2.ResumeLayout(False)
        Me.TabPage3.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Button1 As Button
    Friend WithEvents FileLbl As Label
    Friend WithEvents DrumCheckBox As CheckBox
    Friend WithEvents ChannelTable As DataGridView
    Friend WithEvents Button2 As Button
    Friend WithEvents SaveFileDialog1 As SaveFileDialog
    Friend WithEvents ChannelColumn As DataGridViewTextBoxColumn
    Friend WithEvents MidiVoiceCol As DataGridViewTextBoxColumn
    Friend WithEvents MuteCol As DataGridViewCheckBoxColumn
    Friend WithEvents VoiceCol As DataGridViewComboBoxColumn
    Friend WithEvents TabControl1 As TabControl
    Friend WithEvents TabPage1 As TabPage
    Friend WithEvents TabPage2 As TabPage
    Friend WithEvents TabPage3 As TabPage
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents LinkLabel1 As LinkLabel
End Class
