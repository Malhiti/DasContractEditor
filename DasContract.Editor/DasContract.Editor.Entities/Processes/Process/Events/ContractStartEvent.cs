﻿

using System;
using DasContract.Editor.Entities.Forms;
using DasContract.Editor.Entities.Interfaces;
using DasContract.Editor.Migrator.Interfaces;

namespace DasContract.Editor.Entities.Processes.Process.Events
{
    public class ContractStartEvent : ContractEvent, IDataCopyable<ContractStartEvent>, IMigratableComponent<ContractStartEvent, IMigrator>
    {
        public ContractForm StartForm
        {
            get => startForm?.WithMigrator(Migrator);
            set
            {
                if (value != startForm)
                    Migrator.Notify(() => startForm, e => startForm = e,
                            MigratorMode.Smart);
                startForm = value;
            }
        }
        ContractForm startForm = new ContractForm();

        public void CopyDataFrom(ContractStartEvent source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            startForm = source.StartForm;
        }

        public ContractStartEvent WithMigrator(IMigrator parentMigrator)
        {
            Migrator = parentMigrator;
            return this;
        }
    }
}
