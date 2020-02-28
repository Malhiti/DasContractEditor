﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Bonsai.RazorComponents.MaterialBootstrap.Components.CAlert;
using DasContract.Editor.Entities.DataModels.Entities.Properties.Primitive;
using DasContract.Editor.Entities.DataModels.Entities.Properties.Reference;
using DasContract.Editor.Entities.Integrity.DataModel.Properties;
using DasContract.Editor.Entities.Integrity.DataModel.Properties.Primitive;
using DasContract.Editor.Entities.Integrity.DataModel.Properties.Reference;

namespace DasContract.Editor.Components.Main.Components.CContractEditor.CContractDataModelEditor.CContractPropertyEditor
{
    public partial class ReferenceContractPropertyEditor : ContractPropertyEditorBase
    {

        //--------------------------------------------------
        //                    DELETE
        //--------------------------------------------------
        protected ReferenceContractProperty propertyToDelete = null;
        public override async Task DeletePropertyAsync(int index)
        {
            propertyToDelete = Entity.ReferenceProperties[index];
            deletePropertyAnalysis = Contract.AnalyzeIntegrityOf(propertyToDelete);
            await deleteDialogWindow.OpenAsync();
        }

        public override async Task ConfirmDeleteAsync()
        {
            try
            {
                Contract.RemoveSafely(propertyToDelete);
                alertController.AddAlert("Delete successful", AlertScheme.Success);
                StateHasChanged();
            }
            catch (Exception)
            {
                alertController.AddAlert("Something went wrong :(", AlertScheme.Danger);
            }

            await deleteDialogWindow.CloseAsync();
        }

        //--------------------------------------------------
        //                     ADD
        //--------------------------------------------------
        public override async Task AddPropertyAsync()
        {
            await createDialogWindow.OpenAsync();
        }

        protected override async Task ConfirmAddPropertyAsync()
        {
            var newProperty = newPropertyModel.ToReferenceContractProperty();
            newProperty.Entity = Entity;
            Entity.ReferenceProperties.Add(newProperty);

            await createDialogWindow.CloseAsync();
            alertController.AddAlert("New reference property added successfuly", AlertScheme.Success);
            ResetNewPropertyModel();
        }
    }
}