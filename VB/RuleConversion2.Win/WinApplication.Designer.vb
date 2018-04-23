Namespace RuleConversion2.Win
	Partial Public Class RuleConversion2WindowsFormsApplication
		''' <summary> 
		''' Required designer variable.
		''' </summary>
		Private components As System.ComponentModel.IContainer = Nothing

		''' <summary> 
		''' Clean up any resources being used.
		''' </summary>
		''' <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		Protected Overrides Sub Dispose(ByVal disposing As Boolean)
			If disposing AndAlso (components IsNot Nothing) Then
				components.Dispose()
			End If
			MyBase.Dispose(disposing)
		End Sub

		#Region "Component Designer generated code"

		''' <summary> 
		''' Required method for Designer support - do not modify 
		''' the contents of this method with the code editor.
		''' </summary>
		Private Sub InitializeComponent()
			Me.module1 = New DevExpress.ExpressApp.SystemModule.SystemModule()
			Me.module2 = New DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule()
			Me.module3 = New RuleConversion2.Module.RuleConversion2Module()
			Me.module6 = New DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule()
			Me.sqlConnection1 = New System.Data.SqlClient.SqlConnection()
			Me.conditionalAppearanceModule1 = New DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule()
			CType(Me, System.ComponentModel.ISupportInitialize).BeginInit()
			' 
			' sqlConnection1
			' 
			Me.sqlConnection1.ConnectionString = "Data Source=(local);Initial Catalog=RuleConversion2;Integrated Security=SSPI;Pool" & "ing=false"
			Me.sqlConnection1.FireInfoMessageEventOnUserErrors = False
			' 
			' RuleConversion2WindowsFormsApplication
			' 
			Me.ApplicationName = "RuleConversion2"
			Me.Connection = Me.sqlConnection1
			Me.Modules.Add(Me.module1)
			Me.Modules.Add(Me.module2)
			Me.Modules.Add(Me.module6)
			Me.Modules.Add(Me.conditionalAppearanceModule1)
			Me.Modules.Add(Me.module3)
'			Me.DatabaseVersionMismatch += New System.EventHandler(Of DevExpress.ExpressApp.DatabaseVersionMismatchEventArgs)(Me.RuleConversion2WindowsFormsApplication_DatabaseVersionMismatch)
			CType(Me, System.ComponentModel.ISupportInitialize).EndInit()

		End Sub

		#End Region

		Private module1 As DevExpress.ExpressApp.SystemModule.SystemModule
		Private module2 As DevExpress.ExpressApp.Win.SystemModule.SystemWindowsFormsModule
		Private module3 As RuleConversion2.Module.RuleConversion2Module
		Private module6 As DevExpress.ExpressApp.Objects.BusinessClassLibraryCustomizationModule
		Private sqlConnection1 As System.Data.SqlClient.SqlConnection
		Private conditionalAppearanceModule1 As DevExpress.ExpressApp.ConditionalAppearance.ConditionalAppearanceModule
	End Class
End Namespace
