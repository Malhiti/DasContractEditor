﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using DasContract.Editor.Entities.Interfaces;
using DasContract.Editor.Migrator;
using DasContract.Editor.Migrator.Interfaces;

namespace DasContract.Editor.Entities.DataModels.Entities.Properties
{
    public abstract class ContractProperty : IIdentifiable, INamable, IMigratableComponent<ContractProperty, IMigrator>
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();

        [DisplayName("Property name")]
        //[Required(ErrorMessage = "Property name is required")]
        public string Name
        {
            get => name;
            set
            {
                if (value != name)
                    migrator.Notify(() => name, p => name = p,
                            MigratorMode.Smart);
                name = value;
            }
        }
        string name;

        /// <summary>
        /// Tells if this property is allowed to be the default value or not
        /// </summary>
        [DisplayName("Mandatory property")]
        [Description("Indicates whenever this property must have a value")]
        public bool IsMandatory
        {
            get => isMandatory;
            set
            {
                if (value != isMandatory)
                    migrator.Notify(() => isMandatory, b => isMandatory = b,
                            MigratorMode.Smart);
                isMandatory = value;
            }
        }
        bool isMandatory = true;

        //--------------------------------------------------
        //                  MIGRATOR
        //--------------------------------------------------
        protected IMigrator migrator { get; set; } = new SimpleMigrator();

        public ContractProperty WithMigrator(IMigrator parentMigrator)
        {
            migrator = parentMigrator;
            return this;
        }
    }
}
