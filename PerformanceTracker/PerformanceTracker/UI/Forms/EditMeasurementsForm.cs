using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using Vema.PerfTracker.Database.Domain;
using Vema.PerformanceTracker.UI.Binding;
using Vema.PerfTracker.Database.Helper;
using Vema.PerfTracker.Database.Access;
using Vema.PerfTracker.Database;

namespace Vema.PerformanceTracker.UI.Forms
{
    /// <summary>
    /// Markus Vetsch, 29.02.2012 11:52
    /// Form to edit measurements data for a certain <see cref="Player"/>.
    /// </summary>
    internal partial class EditMeasurementsForm : BaseForm
    {
        private readonly long playerId;
        private readonly Database database;

        private List<PlayerMeasurementListItem> addItems;
        private List<PlayerMeasurementListItem> updateItems;
        private List<PlayerMeasurementListItem> deleteItems;

        private BindingList<string> subCategoryList;

        private Player player;
        private List<FeatureCategory> featureCategories;

        private ListViewItem modificationItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="EditMeasurementsForm"/> class.
        /// </summary>
        /// <param name="playerId">The database ID of the <see cref="Player"/>.</param>
        internal EditMeasurementsForm(long playerId)
        {
            this.playerId = playerId;
            database = Database.Instance;

            addItems = new List<PlayerMeasurementListItem>();
            updateItems = new List<PlayerMeasurementListItem>();
            deleteItems = new List<PlayerMeasurementListItem>();

            subCategoryList = new BindingList<string>();

            InitializeComponent();

            // Preload infos from database    

            PreloadDatabaseInfo(); 

            // Bind unit values and feature category values

            cbxUnit.DataSource = PrepareUnitBindingList();
            cbxCategory.DataSource = featureCategories.Select(category => category.NiceName).ToList();
            cbxSubCategory.DataSource = subCategoryList;
        }

        /// <summary>
        /// Preloads the database info.
        /// </summary>
        private void PreloadDatabaseInfo()
        {
            // Load player and player reference

            player = database.LoadPlayer(playerId);
            database.LoadPlayerReference(player);
            featureCategories = database.LoadAllCategories();

            string playerName = (player != null) ? player.FullName : string.Empty;

            // Set Title of form

            SetText(string.Format("Messdaten für Spieler '{0}' bearbeiten", playerName));
        }

        /// <summary>
        /// Prepares the unit binding list.
        /// </summary>
        /// <returns>The list to bind unit values.</returns>
        private List<string> PrepareUnitBindingList()
        {
            List<string> bindingValues = new List<string>();
            foreach (MeasurementUnit unit in Enum.GetValues(typeof(MeasurementUnit)))
            {
                bindingValues.Add(Measurement.GetUnitAsString(unit));
            }
            
            return bindingValues;
        }

        /// <summary>
        /// Determines whether the selection in the list view control is empty.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if the selection in the list view control is empty; otherwise, <c>false</c>.
        /// </returns>
        private bool IsListSelectionEmpty()
        {
            ListView.SelectedListViewItemCollection items = lvwMeasurements.SelectedItems;
            return items == null || items.Count == 0;
        }

        /// <summary>
        /// Determines whether there are multiple items in the list view control selected.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if there are multiple items in the 
        ///   list view control selected; otherwise, <c>false</c>.
        /// </returns>
        private bool IsMultipleListSelection()
        {
            ListView.SelectedListViewItemCollection items = lvwMeasurements.SelectedItems;
            return items != null && items.Count > 1;
        }

        /// <summary>
        /// Updates the controls according to the current <see cref="EditMode"/>.
        /// </summary>
        /// <param name="mode">The <see cref="EditMode"/>.</param>
        private void UpdateControls(EditMode mode)
        {
            // Set button states

            UpdateButtonStates(mode);

            // Update other controls

            UpdateOtherControls(mode);
        }

        /// <summary>
        /// Updates the other controls according to the specified <paramref name="mode"/>.
        /// </summary>
        /// <param name="mode">The mode.</param>
        private void UpdateOtherControls(EditMode mode)
        {
            if (mode == EditMode.None)
            {
                txtValue.Clear();

                txtValue.Enabled = false;
                cbxUnit.Enabled = false;

                cbxCategory.Enabled = false;

                cbxSubCategory.Enabled = false;

                rtxtRemark.Clear();
                rtxtRemark.Enabled = false;
            }
            else
            {
                txtValue.Enabled = true;
                cbxUnit.Enabled = true;
                cbxCategory.Enabled = true;
                cbxSubCategory.Enabled = true;
                rtxtRemark.Enabled = true;
            }            
        }

        /// <summary>
        /// Updates the button states according to the specified <paramref name="mode"/>.
        /// </summary>
        /// <param name="mode">The <see cref="EditMode"/> to be respected.</param>
        private void UpdateButtonStates(EditMode mode)
        {
            if (mode == EditMode.Create)
            {
                // Create edit mode => enable only Reset / Add buttons

                btnReset.Enabled = true;

                btnNew.Enabled = false;
                btnAdd.Enabled = true;

                btnModify.Enabled = false;
                btnConfirm.Enabled = false;

                // Remove action in edit mode prohibited

                btnRemove.Enabled = false;

                // Save action in edit mode prohibited

                btnSave.Enabled = false;
            }
            else if (mode == EditMode.Update)
            {
                // Update edit mode => enable only Reset / Confirm buttons

                btnReset.Enabled = true;

                btnNew.Enabled = false;
                btnAdd.Enabled = false;

                btnModify.Enabled = false;
                btnConfirm.Enabled = true;

                // Remove action in edit mode prohibited

                btnRemove.Enabled = false;

                // Save action in edit mode prohibited

                btnSave.Enabled = false;
            }
            else
            {
                // No edit mode => enable only New / Modify buttons

                btnReset.Enabled = false;

                btnAdd.Enabled = false;
                btnNew.Enabled = true;

                btnModify.Enabled = true;
                btnConfirm.Enabled = false;

                btnRemove.Enabled = true;
                btnSave.Enabled = false;
            }
        }

        /// <summary>
        /// Inserts the existing values for specified <paramref name="item"/>.
        /// </summary>
        /// <param name="item">The <see cref="PlayerMeasurementListItem"/> to insert values for.</param>
        private void InsertExistingValues(PlayerMeasurementListItem item)
        {
            txtValue.Text = item.Value.ToString();
            cbxUnit.SelectedItem = GetUnit(item);
            cbxCategory.SelectedItem = item.Category;
            cbxSubCategory.SelectedItem = item.SubCategory;
            rtxtRemark.Text = item.Remark;
        }

        /// <summary>
        /// Gets the category selection as <see cref="Pair"/> of instances.
        /// </summary>
        /// <returns>The <see cref="Pair"/> of selected combination of 
        /// <see cref="FeatureCategory"/> / <see cref="FeatureSubCategory"/>.</returns>
        private Pair<FeatureCategory, FeatureSubCategory> GetCategorySelection()
        {
            FeatureCategory category = featureCategories.Find(cat => cat.NiceName == cbxCategory.SelectedItem.ToString());

            if (category != null)
            {
                FeatureSubCategory subCategory = 
                    category.SubCategories.Find(cat => cat.NiceName == cbxSubCategory.SelectedItem.ToString());

                return new Pair<FeatureCategory, FeatureSubCategory>(category, subCategory);
            }

            return null;
        }

        /// <summary>
        /// Gets the unit for specified <paramref name="item"/>.
        /// </summary>
        /// <param name="item">The <see cref="PlayerMeasurementListItem"/> to retrieve the unit for.</param>
        /// <returns>The unit as <see cref="string"/>.</returns>
        private string GetUnit(PlayerMeasurementListItem item)
        {
            return Measurement.GetUnitAsString(item.Unit);
        }

        /// <summary>
        /// Gets the <see cref="MeasurementUnit"/> for specified unit as <see cref="string"/>.
        /// </summary>
        /// <param name="unit">The unit to be converted.</param>
        /// <returns>The corresponding <see cref="MeasurementUnit"/></returns>
        private MeasurementUnit GetUnit(string unit)
        {
            return Measurement.Parse(unit);
        }

        /// <summary>
        /// Validates the mandatory field in the form.
        /// </summary>
        /// <returns><c>true</c>, if the mandatory field value is ok; otherwise <c>false</c>.</returns>
        private bool ValidateMandatoryField()
        {
            return !string.IsNullOrEmpty(txtValue.Text) 
                    && InputValueValidator.IsValidPositiveDouble(txtValue.Text);
        }

        /// <summary>
        /// Creates the new <see cref="Measurement"/> from specified <paramref name="item"/>.
        /// </summary>
        /// <param name="item">The new <see cref="Measurement"/> item.</param>
        /// <returns>The new <see cref="Measurement"/> to be inserted.</returns>
        private Measurement CreateNewMeasurement(PlayerMeasurementListItem item)
        {
            FeatureCategory category = featureCategories.Find(cat => cat.NiceName == item.Category);
            FeatureSubCategory subCategory = category.SubCategories.Find(cat => cat.NiceName == item.SubCategory);

            MeasurementDao dao = (MeasurementDao) DaoFactory.CreateDao<Measurement>();
            dao.Value = item.Value;
            dao.Unit = item.Unit;
            dao.Remark = item.Remark;
            dao.Timestamp = item.Timestamp;

            dao.SubCategoryDao = subCategory.Dao;
            dao.PlayerReferenceDao = player.Reference.Dao;

            return (Measurement) dao.CreateDomainObject();
        }

        /// <summary>
        /// Creates a <see cref="Measurement"/> from specified <paramref name="item"/> to be updated.
        /// </summary>
        /// <param name="item">The <see cref="Measurement"/> item to be updated.</param>
        /// <returns>The <see cref="Measurement"/> to be updated.</returns>
        private Measurement CreateUpdateMeasurement(PlayerMeasurementListItem item)
        {
            FeatureCategory category = featureCategories.Find(cat => cat.NiceName == item.Category);
            FeatureSubCategory subCategory = category.SubCategories.Find(cat => cat.NiceName == item.SubCategory);

            MeasurementDao dao = item.Measurement.Dao;
            dao.Value = item.Value;
            dao.Remark = item.Remark;
            dao.Unit = item.Unit;
            dao.Timestamp = item.Timestamp;

            dao.SubCategoryDao = subCategory.Dao;
            dao.PlayerReferenceDao = player.Reference.Dao;

            return (Measurement) dao.CreateDomainObject();
        }

        /// <summary>
        /// Creates the list of <see cref="Measurement"/> items to be updated.
        /// </summary>
        /// <returns>The list of <see cref="Measurement"/> items to be updated.</returns>
        private IEnumerable<Measurement> CreateUpdateList()
        {
            List<Measurement> measurements = new List<Measurement>();

            foreach (PlayerMeasurementListItem item in updateItems)
            {
                measurements.Add(CreateUpdateMeasurement(item));
            }

            return measurements;
        }

        /// <summary>
        /// Creates the list of new <see cref="Measurement"/> items to be newly inserted.
        /// </summary>
        /// <returns>The list of new <see cref="Measurement"/> items to be inserted.</returns>
        private IEnumerable<Measurement> CreateNewList()
        {
            List<Measurement> measurements = new List<Measurement>();

            foreach (PlayerMeasurementListItem item in addItems)
            {
                measurements.Add(CreateNewMeasurement(item));
            }

            return measurements;
        }

        /// <summary>
        /// Creates the delete list of measurement items to be deleted.
        /// </summary>
        /// <returns>The list of database ID of items to be deleted.</returns>
        private IEnumerable<Measurement> CreateDeleteList()
        {
            return deleteItems.Select(item => item.Measurement);
        }

        #region Event Handling

        /// <summary>
        /// Handles the Load event of the EditMeasurementsForm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void EditMeasurementsForm_Load(object sender, EventArgs e)
        {
            List<Measurement> measurements = database.LoadMeasurements(player);

            // Fill list view with existing measurements

            foreach (Measurement measurement in measurements)
            {
                PlayerMeasurementListItem measurementItem = new PlayerMeasurementListItem(measurement);
                ListViewItem listItem = lvwMeasurements.Items.Add(new ListViewItem(measurementItem.ToArray()));
                listItem.Tag = measurementItem;
                listItem.Name = measurementItem.Id.ToString();
            }

            // Auto adjust size of list view columns

            Gui.AutoAdjustListViewColumns(lvwMeasurements);
        }

        /// <summary>
        /// Handles the Click event of the btnModify control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnModify_Click(object sender, EventArgs e)
        {
            if (IsListSelectionEmpty())
            {
                Gui.ShowInformation("Keine Messdaten selektiert",
                    "Bitte wählen Sie genau einen Datensatz zur Bearbeitung aus.");

                return;
            }
            if (IsMultipleListSelection())
            {
                Gui.ShowInformation("Mehrere Messdaten selektiert",
                    "Bitte wählen Sie genau einen Datensatz zur Bearbeitung aus.");

                return;
            }

            ListViewItem selectedItem = lvwMeasurements.SelectedItems[0];
            PlayerMeasurementListItem item = (PlayerMeasurementListItem) selectedItem.Tag;

            // Store modification item for later write back

            modificationItem = selectedItem;            

            // Insert existing values of selected item

            InsertExistingValues(item);

            // Update control states

            UpdateControls(EditMode.Update);
        }

        /// <summary>
        /// Handles the Click event of the btnNew control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnNew_Click(object sender, EventArgs e)
        {
            UpdateControls(EditMode.Create);
        }

        /// <summary>
        /// Handles the Click event of the btnAdd control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateMandatoryField())
            {
                // Create measurement item

                PlayerMeasurementListItem item = new PlayerMeasurementListItem();
                item.Value = double.Parse(txtValue.Text);
                item.Unit = GetUnit(cbxUnit.SelectedItem.ToString());
                item.Remark = rtxtRemark.Text;

                // Get category selection

                Pair<FeatureCategory, FeatureSubCategory> categories = GetCategorySelection();

                if (categories != null)
                {
                    item.Category = categories.Left.NiceName;
                    item.SubCategory = categories.Right.NiceName;
                }

                item.Timestamp = DateTime.Now;

                // Add item to list to be added

                addItems.Add(item);                

                // Add new list view item

                ListViewItem listItem = lvwMeasurements.Items.Add(new ListViewItem(item.ToArray()));
                listItem.Tag = item;
                listItem.Name = item.Id.ToString();

                // Auto adjust column sizes

                Gui.AutoAdjustListViewColumns(lvwMeasurements);

                // Release edit mode

                UpdateControls(EditMode.None);
            }
            else
            {
                Gui.ShowInformation("Ungültige Eingabe", "Pflichtfeld enthält ungültigen Eingabewert.");
            } 
        }

        /// <summary>
        /// Handles the Click event of the btnConfirm control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnConfirm_Click(object sender, EventArgs e)
        {
            if (ValidateMandatoryField())
            {
                // Verify, that modification item not null

                if (modificationItem == null)
                {
                    Gui.ShowError("Unerwarteter Fehler",
                        "Das ausgewählte Element konnte nicht zugeordnet werden!");
                    return;
                }

                PlayerMeasurementListItem item = (PlayerMeasurementListItem) modificationItem.Tag;

                item.Value = double.Parse(txtValue.Text);
                item.Unit = GetUnit(cbxUnit.SelectedItem.ToString());
                item.Remark = rtxtRemark.Text;

                // Update categories; timestamp remains untouched

                Pair<FeatureCategory, FeatureSubCategory> categories = GetCategorySelection();

                if (categories != null)
                {
                    item.Category = categories.Left.NiceName;
                    item.SubCategory = categories.Right.NiceName;
                }

                // Add modification item to update items, if not new item

                if (item.Id != -1)
                {
                    updateItems.Add(item);
                }

                int index = lvwMeasurements.Items.IndexOf(modificationItem);
                lvwMeasurements.Items.Remove(modificationItem);

                ListViewItem newItem = new ListViewItem(item.ToArray());
                newItem.Tag = item;
                newItem.Name = item.Id.ToString();
                lvwMeasurements.Items.Insert(index, newItem);

                // Release edit mode

                UpdateControls(EditMode.None);
            }
            else
            {
                Gui.ShowInformation("Ungültige Eingabe", "Pflichtfeld enthält ungültigen Eingabewert.");
            } 
        }

        /// <summary>
        /// Handles the Click event of the btnRemove control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnRemove_Click(object sender, EventArgs e)
        {
            // Only execute, if there are item(s) selected in the list view

            if (!IsListSelectionEmpty())
            {
                foreach (ListViewItem listItem in lvwMeasurements.SelectedItems)
                {
                    PlayerMeasurementListItem item = (PlayerMeasurementListItem) listItem.Tag;

                    // new item indicated by key -1

                    if (listItem.Name == "-1")
                    {
                        // Delete from list to be added, if new item

                        addItems.Remove(item);
                    }
                    else
                    {
                        // Add to delete list, if not a new item
                        // Remove from update list

                        updateItems.Remove(item);
                        deleteItems.Add(item);
                    }

                    // Remove from list view in each case

                    lvwMeasurements.Items.Remove(listItem);
                }

                // Auto adjust column sizes

                Gui.AutoAdjustListViewColumns(lvwMeasurements);
            }
        }

        /// <summary>
        /// Handles the Click event of the btnReset control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            // Reset modification item

            if (modificationItem != null)
            {
                modificationItem = null;
            }

            UpdateControls(EditMode.None);
        }

        /// <summary>
        /// Handles the Click event of the btnCancel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (Gui.AskQuestion("Änderungen verwerfen", "Wollen Sie wirklich alle Änderungen verwerfen und den Dialog verlassen?"))
            {
                Close();
            }
        }

        /// <summary>
        /// Handles the Click event of the btnSave control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (Gui.AskQuestion("Änderungen speichern", "Wollen Sie die vorgenommenen Änderungen speichern und den Dialog verlassen?"))
            {
                IEnumerable<Measurement> newList = CreateNewList();
                IEnumerable<Measurement> updateList = CreateUpdateList();
                IEnumerable<Measurement> deleteList = CreateDeleteList();

                // Perform database operations

                database.SaveAllMeasurements(newList);
                database.UpdateAllMeasurement(updateList);
                database.DeleteAllMeasurement(deleteList);

                Close();
            }
        }

        /// <summary>
        /// Handles the SelectedIndexChanged event of the cbxCategory control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void cbxCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            subCategoryList.Clear();

            string selectedCategoryName = cbxCategory.SelectedItem.ToString();
            FeatureCategory featureCategory = featureCategories.Single(category => category.NiceName == selectedCategoryName);
            IEnumerable<string> subCategories = featureCategory.SubCategories.Select(subCategory => subCategory.NiceName);

            foreach (string subCategory in subCategories)
            {
                subCategoryList.Add(subCategory);
            }
        }

        /// <summary>
        /// Handles the Validated event a <see cref="Control"/> expecting a <see cref="double"/> input value.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ValidatedDouble(object sender, EventArgs e)
        {
            Control control = (Control) sender;
            if (!string.IsNullOrEmpty(control.Text))
            {
                // Validate for a double value

                if (InputValueValidator.IsValidPositiveDouble(control.Text))
                {
                    Gui.ResetTextboxFromError(control);
                }
                else
                {
                    Gui.ShowMessage("Ungültige Eingabe", "Eingabe muss eine positive Zahl (mit / ohne Kommastellen) sein.");
                    Gui.SetTextboxError(control);
                }
            }
            else
            {
                Gui.ResetTextboxFromError(control);
            }
        }

        #endregion

        
    }
}
