namespace Crool.AddIn.Controls
{
    using System.Collections.Generic;
    using System;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;
    using Crool.AddIn.Business.DataAcess;
    using EnvDTE;
    using EnvDTE80;
    
    [GuidAttribute("1FD0E084-B116-4A94-9E85-0BE6A3DC3181"), ProgId("Crool.AddIn.MyControl")]
    public partial class MyControl : UserControl
    {
        /// <summary>
        /// Gets or sets host (visual studio instance) of this user control.
        /// </summary>
        public DTE2 Application { get; set; }

        private bool IsInCreateMode { get; set; }

        private Business.Entities.CroolProject ActiveCroolProject { get; set; }

        private Document CurrentActiveDocument { get; set; }

        public MyControl()
        {
            InitializeComponent();

            this.LoadCroolProjectsCombo();
        }

        public void Initialize()
        {
            this.Application.Events.WindowEvents.WindowActivated += new _dispWindowEvents_WindowActivatedEventHandler(this.WindowEvents_WindowActivated);
        }

        void WindowEvents_WindowActivated(Window infocus, Window outfocus)
        {
            if (infocus.Document != null && this.CurrentActiveDocument != this.Application.ActiveDocument)
            {
                string name = infocus.Document.FullName;

                // Switch to another document.
                this.CurrentActiveDocument = this.Application.ActiveDocument;

                var file = GetLinkedFile(infocus.Document);
                
                //if (file == null)
                //{
                //    var activeProject = infocus.Document.ProjectItem.ContainingProject;
                //    string pathToProject = activeProject.FullName.Substring(0, activeProject.FullName.LastIndexOf('\\'));

                //    context.Files.Add(new Business.Entities.File
                //    {
                //        FileName = GetRelativeFilePath(pathToProject, infocus.Document.FullName),
                //    });
                //}

                this.LoadReviews(file);
            }
        }

        private void btnLoad_Click(object sender, System.EventArgs e)
        {
            this.LoadCroolProject();
        }

        private void CreateCroolProject(string friendlyName)
        {
            try
            {
                var croolProject = new Business.Entities.CroolProject
                {
                    Name = friendlyName
                };

                foreach (Project p in this.Application.Solution.Projects)
                {
                    this.AddProjectToCroolProject(croolProject, p);
                }

                using (var context = new CroolContext(Config.ConnectionString))
                {
                    context.CroolProjects.Add(croolProject);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Something went wrong.\nException message:" + ex.Message);
            }
        }

        private void AddProjectToCroolProject(Business.Entities.CroolProject croolProject, Project p)
        {
            string projectName = string.IsNullOrEmpty(p.FullName) ? p.Name : p.FullName;

            if (!string.IsNullOrEmpty(projectName))
            {
                int lastSlash = projectName.LastIndexOf('\\');
                lastSlash = lastSlash == -1 ? 0 : lastSlash;
                string pathToProject = projectName;

                if (lastSlash != 0)
                {
                    pathToProject = projectName.Substring(0, lastSlash);
                }

                var project = new Business.Entities.Project
                {
                    Name = p.UniqueName
                };

                croolProject.Projects.Add(project);

                this.LoadProjectFiles(croolProject, p.ProjectItems, project, pathToProject);
            }
        }

        private void LoadProjectFiles(Business.Entities.CroolProject crool, ProjectItems items, Business.Entities.Project project, string pathToProject)
        {
            if (items == null) return;

            foreach (ProjectItem i in items)
            {
                // If it's a project file.
                if (i.SubProject != null)
                {
                    AddProjectToCroolProject(crool, i.SubProject);
                    continue;
                }

                this.LoadProjectFiles(crool, i.ProjectItems, project, pathToProject);

                var file = new Business.Entities.File();

                if (i.Properties != null)
                {
                    foreach (Property property in i.Properties)
                    {
                        if (property.Name == "FullPath")
                        {
                            file.FileName = property.Value as string;

                            if (!string.IsNullOrEmpty(file.FileName))
                            {
                                file.FileName = GetRelativeFilePath(pathToProject, file.FileName);
                            }
                        }
                    }
                }

                if (!string.IsNullOrEmpty(file.FileName))
                {
                    project.Files.Add(file);
                }
            }
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            // If no solution or project is open.
            if (!this.Application.Solution.IsOpen)
            {
                MessageBox.Show("You must open a solution/project before being able to create Crool project for it.");
                return;
            }

            this.ToggleCreateMode();
        }

        private void btnConfirmCreate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtNewName.Text))
            {
                MessageBox.Show("Please provide a name you want to use for the new Crool project.");
                return;
            }

            using (var context = new CroolContext(Config.ConnectionString))
            {
                if (!context.CroolProjects.Where(c => c.Name == this.txtNewName.Text).Any())
                {
                    this.CreateCroolProject(this.txtNewName.Text);
                    this.ToggleCreateMode();
                    this.LoadCroolProjectsCombo();
                    this.LoadCroolProject();
                }
                else
                {
                    MessageBox.Show("Crool project with the same name already exists. Please use another name.");
                }
            }
        }

        private void ToggleCreateMode()
        {
            if (this.IsInCreateMode)
            {
                this.txtNewName.Visible = false;
                this.btnConfirmCreate.Visible = false;
                this.btnCreate.Text = "Create New";

                this.IsInCreateMode = false;
            }
            else
            {
                this.txtNewName.Visible = true;
                this.btnConfirmCreate.Visible = true;
                this.btnCreate.Text = "Cancel";

                this.IsInCreateMode = true;
            }
        }

        private void LoadCroolProjectsCombo()
        {
            using (var context = new CroolContext(Config.ConnectionString))
            {
                this.cmbProjects.DataSource = context.CroolProjects.ToList();
                this.cmbProjects.DisplayMember = "Name";
                this.cmbProjects.ValueMember = "Id";
            }
        }

        private string GetRelativeFilePath(string project, string file)
        {
            //project = project.Replace(project.Substring(project.LastIndexOf('\\')), string.Empty);
            return file.Replace(project, string.Empty);
        }

        private Business.Entities.File GetLinkedFile(Document document)
        {
            if (this.cmbProjects.SelectedValue != null)
            {
                var project = document.ProjectItem.ContainingProject;
                string pathToProject = project.FullName.Substring(0, project.FullName.LastIndexOf('\\'));

                var path = GetRelativeFilePath(pathToProject, document.FullName);

                using (var context = new CroolContext(Config.ConnectionString))
                {
                    var file =
                        context.Files.Where(
                            f =>
                            f.Project.CroolProject_Id == (int)this.cmbProjects.SelectedValue &&
                            f.FileName == path).FirstOrDefault();

                    return file;
                }
            }

            return null;
        }

        private void btnSaveReview_Click(object sender, EventArgs e)
        {
            if (this.Application.ActiveDocument != null)
            {
                dynamic selection = this.Application.ActiveDocument.Selection;
                var file = GetLinkedFile(this.Application.ActiveDocument);

                using (var context = new CroolContext(Config.ConnectionString))
                {
                    var review = new Business.Entities.Review
                    {
                        Text = this.txtReviewText.Text,
                        From = this.txtFrom.Text,
                        To = this.txtTo.Text,
                        StartLine = selection.CurrentLine,
                        EndLine = selection.BottomLine,
                        File_Id = file.Id
                    };

                    context.Reviews.Add(review);
                    context.SaveChanges();
                }

                this.tabControl1.SelectTab(0);
                this.LoadReviews(file);
            }
        }

        private void LoadCroolProject()
        {
            if (this.cmbProjects.SelectedValue == null)
            {
                MessageBox.Show("Please select the Crool project you wish to load.");
                return;
            }

            using (var context = new CroolContext(Config.ConnectionString))
            {
                int croolProjectId = (int)this.cmbProjects.SelectedValue;
                this.ActiveCroolProject = context.CroolProjects.Where(p => p.Id == croolProjectId).FirstOrDefault();
            }
        }

        private void LoadReviews(Business.Entities.File file)
        {
            if (file == null)
            {
                return;
            }

            // Save all pending changes.
            this.dataGridView1.EndEdit();
            this.reviewsBindingSource.EndEdit();
            this.dataSet11.AcceptChanges();
            this.reviewsTableAdapter.Update(this.dataSet11.Reviews);

            this.reviewsTableAdapter.FillByFileId(this.dataSet11.Reviews, file.Id);
            this.reviewsTableAdapter.FillUnresolvedByFile_Id(this.dataSet12.Reviews, file.Id);

            //using (var context = new CroolContext(Config.ConnectionString))
            //{
            //    var reviews = context.Reviews.Where(r => r.File_Id == file.Id).ToList();

            //    this.dataGridView1.DataSource = reviews;

            //    try
            //    {
            //        this.dataGridView1.Columns["Id"].Visible = false;
            //        this.dataGridView1.Columns["File_Id"].Visible = false;
            //        this.dataGridView1.Columns["Comments"].Visible = false;
            //        this.dataGridView1.Columns["File"].Visible = false;
            //        this.dataGridView1.Columns["Text"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            //    }
            //    catch
            //    {
            //        // Ignore exception.
            //    }
            //}
        }
    }
}
