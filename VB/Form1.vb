Imports System
Imports System.Drawing
Imports System.Collections
Imports System.ComponentModel
Imports System.Windows.Forms
Imports System.Data
Imports DevExpress.XtraTreeList
Imports DevExpress.XtraTreeList.Nodes

Namespace DragDropReorder
    ''' <summary>
    ''' Summary description for Form1.
    ''' </summary>
    Public Class Form1
        Inherits System.Windows.Forms.Form

        Private WithEvents treeList1 As DevExpress.XtraTreeList.TreeList
        ''' <summary>
        ''' Required designer variable.
        ''' </summary>
        Private components As System.ComponentModel.Container = Nothing

        Public Sub New()
            '
            ' Required for Windows Form Designer support
            '
            InitializeComponent()

            '
            ' TODO: Add any constructor code after InitializeComponent call
            '
        End Sub

        ''' <summary>
        ''' Clean up any resources being used.
        ''' </summary>
        Protected Overrides Sub Dispose(ByVal disposing As Boolean)
            If disposing Then
                If components IsNot Nothing Then
                    components.Dispose()
                End If
            End If
            MyBase.Dispose(disposing)
        End Sub

        #Region "Windows Form Designer generated code"
        ''' <summary>
        ''' Required method for Designer support - do not modify
        ''' the contents of this method with the code editor.
        ''' </summary>
        Private Sub InitializeComponent()
            Me.treeList1 = New DevExpress.XtraTreeList.TreeList()
            DirectCast(Me.treeList1, System.ComponentModel.ISupportInitialize).BeginInit()
            Me.SuspendLayout()
            ' 
            ' treeList1
            ' 
            Me.treeList1.Dock = System.Windows.Forms.DockStyle.Fill
            Me.treeList1.Location = New System.Drawing.Point(0, 0)
            Me.treeList1.Name = "treeList1"
            Me.treeList1.Size = New System.Drawing.Size(422, 277)
            Me.treeList1.TabIndex = 0
            ' 
            ' Form1
            ' 
            Me.AutoScaleBaseSize = New System.Drawing.Size(5, 13)
            Me.ClientSize = New System.Drawing.Size(422, 277)
            Me.Controls.Add(Me.treeList1)
            Me.Name = "Form1"
            Me.Text = "Form1"
            DirectCast(Me.treeList1, System.ComponentModel.ISupportInitialize).EndInit()
            Me.ResumeLayout(False)

        End Sub
        #End Region

        ''' <summary>
        ''' The main entry point for the application.
        ''' </summary>
        <STAThread> _
        Shared Sub Main()
            Application.Run(New Form1())
        End Sub

        Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
            Dim tempVar As New DevExpress.XtraTreeList.Design.XViews(treeList1)
            treeList1.OptionsBehavior.DragNodes = True
        End Sub

        Private Function GetDragDropEffect(ByVal tl As TreeList, ByVal dragNode As TreeListNode) As DragDropEffects
            Dim targetNode As TreeListNode
            Dim p As Point = tl.PointToClient(MousePosition)
            targetNode = tl.CalcHitInfo(p).Node

            If dragNode IsNot Nothing AndAlso targetNode IsNot Nothing AndAlso dragNode IsNot targetNode AndAlso dragNode.ParentNode Is targetNode.ParentNode Then
                Return DragDropEffects.Move
            Else
                Return DragDropEffects.None
            End If
        End Function

        Private Sub treeList1_DragOver(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles treeList1.DragOver
            Dim dragNode As TreeListNode = TryCast(e.Data.GetData(GetType(TreeListNode)), TreeListNode)
            e.Effect = GetDragDropEffect(TryCast(sender, TreeList), dragNode)
        End Sub

        Private Sub treeList1_DragDrop(ByVal sender As Object, ByVal e As System.Windows.Forms.DragEventArgs) Handles treeList1.DragDrop
            Dim dragNode, targetNode As TreeListNode
            Dim tl As TreeList = TryCast(sender, TreeList)
            Dim p As Point = tl.PointToClient(New Point(e.X, e.Y))

            dragNode = TryCast(e.Data.GetData(GetType(TreeListNode)), TreeListNode)
            targetNode = tl.CalcHitInfo(p).Node

            tl.SetNodeIndex(dragNode, tl.GetNodeIndex(targetNode))
            e.Effect = DragDropEffects.None
        End Sub

        Private Sub treeList1_CalcNodeDragImageIndex(ByVal sender As Object, ByVal e As DevExpress.XtraTreeList.CalcNodeDragImageIndexEventArgs) Handles treeList1.CalcNodeDragImageIndex
            Dim tl As TreeList = TryCast(sender, TreeList)
            If GetDragDropEffect(tl, tl.FocusedNode) = DragDropEffects.None Then
                e.ImageIndex = -1 ' no icon
            Else
                e.ImageIndex = 1 ' the reorder icon (a curved arrow)
            End If
        End Sub
    End Class
End Namespace