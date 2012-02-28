using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vema.PerfTracker.Database.Domain;
using Vema.PerfTracker.Database.Helper;
using Vema.PerfTracker.Database.Config;

namespace Vema.PerfTracker.Database.Service
{
    /// <summary>
    /// Markus Vetsch, 20.02.2012 14:27
    /// Service class for loading <see cref="FeatureCategory"/> instances.
    /// </summary>
    public class FeatureCategoryService : BaseService<FeatureCategory>
    {
        private static FeatureCategoryService instance;     // Singleton

        /// <summary>
        /// Prevents a default instance of the <see cref="FeatureCategoryService"/> class from being created.
        /// Use <see cref="FeatureCategoryService.GetInstance(Db)"/> to access the singleton instance instead.
        /// </summary>
        /// <param name="database">The underlying <paramref name="database"/> implementation.</param>
        private FeatureCategoryService(Db database)
            : base(database)
        { 
        }

        /// <summary>
        /// Gets the singleton <see cref="FeatureCategoryService"/> instance.
        /// </summary>
        /// <param name="database">The underlying <paramref name="database"/> implementation.</param>
        /// <returns>The singleton <see cref="FeatureCategoryService"/>.</returns>
        public static FeatureCategoryService GetInstance(Db database)
        {
            if (instance == null)
            {
                instance = new FeatureCategoryService(database);
            }

            return instance;
        }

        #region Service Functions - Load

        /// <summary>
        /// Loads the <see cref="FeatureSubCategory"/> by specified database ID.
        /// </summary>
        /// <param name="id">The database ID of the <see cref="FeatureSubCategory"/>.</param>
        /// <returns>
        /// The <see cref="FeatureSubCategory"/> for specified database ID
        /// or <c>null</c>, if no matching item found.
        /// </returns>
        public FeatureSubCategory LoadSubCategoryById(long id)
        {
            FeatureSubCategory subCategory = database.LoadById<FeatureSubCategory>(id);
            FeatureCategory category = LoadById(subCategory.ParentCategory.Id);

            subCategory.ParentCategory = category;

            return subCategory;
        }

        /// <summary>
        /// Loads the <see cref="FeatureCategory"/> by specified database ID.
        /// </summary>
        /// <param name="id">The database ID.</param>
        /// <returns>The <see cref="FeatureCategory"/> for specified database ID
        /// or <c>null</c>, if no matching item found.</returns>
        public override FeatureCategory LoadById(long id)
        {
            FeatureCategory category = base.LoadById(id);
            LoadSubCategories(category);

            return category;
        }

        /// <summary>
        /// Loads all <see cref="FeatureCategory"/> and their referenced <see cref="FeatureSubCategory"/> items from database.
        /// </summary>
        /// <returns>The complete list of <see cref="FeatureCategory"/> items.</returns>
        public override List<FeatureCategory> LoadAll()
        {
            List<FeatureCategory> categories = base.LoadAll();
            categories.ForEach(category => LoadSubCategories(category));

            return categories;
        }

        /// <summary>
        /// Loads the <see cref="FeatureCategory"/> matching to specified <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The category name to be matched.</param>
        /// <returns>The <see cref="FeatureCategory"/> matching to specified <paramref name="name"/>.</returns>
        public FeatureCategory LoadByName(string name)
        {
            DbTableMap map = database.GetMap(typeof(FeatureCategory));
            string niceNameColumn = map.GetColumnForMemberName("NiceName");

            QueryConstraint constraint = new QueryConstraint(niceNameColumn, name, QueryOperator.Equal);

            FeatureCategory category = database.LoadAll<FeatureCategory>(constraint).Single();
            LoadSubCategories(category);

            return category;
        }

        /// <summary>
        /// Loads all the <see cref="FeatureSubCategory"/> items for specified <paramref name="category"/>.
        /// </summary>
        /// <param name="category">The <see cref="FeatureCategory"/>, 
        /// which <see cref="FeatureSubCategory"/> items shall be loaded for.</param>
        private void LoadSubCategories(FeatureCategory category)
        {
            if (category == null)
            {
                throw new ArgumentNullException("category");
            }

            for (int i = 0; i < category.SubCategories.Count; i++)
            {
                FeatureSubCategory current = category.SubCategories[i];
                category.SubCategories[i] = database.LoadById<FeatureSubCategory>(current.Id);
            }
        }

        #endregion

    }
}
