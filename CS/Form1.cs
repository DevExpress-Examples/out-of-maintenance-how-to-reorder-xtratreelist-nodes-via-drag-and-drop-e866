using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;

namespace DragDropReorder {
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class Form1 : System.Windows.Forms.Form {
        private DevExpress.XtraTreeList.TreeList treeList1;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public Form1() {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose( bool disposing ) {
            if( disposing ) {
                if (components != null) {
                    components.Dispose();
                }
            }
            base.Dispose( disposing );
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.treeList1 = new DevExpress.XtraTreeList.TreeList();
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).BeginInit();
            this.SuspendLayout();
            // 
            // treeList1
            // 
            this.treeList1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeList1.Location = new System.Drawing.Point(0, 0);
            this.treeList1.Name = "treeList1";
            this.treeList1.Size = new System.Drawing.Size(422, 277);
            this.treeList1.TabIndex = 0;
            this.treeList1.CalcNodeDragImageIndex += new DevExpress.XtraTreeList.CalcNodeDragImageIndexEventHandler(this.treeList1_CalcNodeDragImageIndex);
            this.treeList1.DragOver += new System.Windows.Forms.DragEventHandler(this.treeList1_DragOver);
            this.treeList1.DragDrop += new System.Windows.Forms.DragEventHandler(this.treeList1_DragDrop);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(422, 277);
            this.Controls.Add(this.treeList1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.treeList1)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.Run(new Form1());
        }

        private void Form1_Load(object sender, System.EventArgs e) {
            new DevExpress.XtraTreeList.Design.XViews(treeList1);
            treeList1.OptionsBehavior.DragNodes = true;
        }

        private DragDropEffects GetDragDropEffect(TreeList tl, TreeListNode dragNode) {
            TreeListNode targetNode;
            Point p = tl.PointToClient(MousePosition);
            targetNode = tl.CalcHitInfo(p).Node;

            if(dragNode != null && targetNode != null 
                && dragNode != targetNode
                && dragNode.ParentNode == targetNode.ParentNode)
                return DragDropEffects.Move;
            else
                return DragDropEffects.None;
        }

        private void treeList1_DragOver(object sender, System.Windows.Forms.DragEventArgs e) {
            TreeListNode dragNode = e.Data.GetData(typeof(TreeListNode)) as TreeListNode;
            e.Effect = GetDragDropEffect(sender as TreeList, dragNode);
        }

        private void treeList1_DragDrop(object sender, System.Windows.Forms.DragEventArgs e) {
            TreeListNode dragNode, targetNode;
            TreeList tl = sender as TreeList;
            Point p = tl.PointToClient(new Point(e.X, e.Y));
			
            dragNode = e.Data.GetData(typeof(TreeListNode)) as TreeListNode;
            targetNode = tl.CalcHitInfo(p).Node;
			
            tl.SetNodeIndex(dragNode, tl.GetNodeIndex(targetNode));
            e.Effect = DragDropEffects.None;
        }

        private void treeList1_CalcNodeDragImageIndex(object sender, DevExpress.XtraTreeList.CalcNodeDragImageIndexEventArgs e) {
            TreeList tl = sender as TreeList;
            if(GetDragDropEffect(tl, tl.FocusedNode) == DragDropEffects.None)
                e.ImageIndex = -1;  // no icon
            else
                e.ImageIndex = 1;  // the reorder icon (a curved arrow)
        }
    }
}