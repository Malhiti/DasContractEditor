﻿

using System;
using DasContract.Editor.Entities.Interfaces;
using DasContract.Editor.Migrator.Interfaces;

namespace DasContract.Editor.Entities.Processes.Process.Activities
{
    public class ContractScriptActivity : ContractActivity, IDataCopyable<ContractScriptActivity>, IMigratableComponent<ContractScriptActivity, IMigrator>
    {
        public string Script
        {
            get => script;
            set
            {
                if (value != script)
                    Migrator.Notify(() => script, e => script = e);
                script = value;
            }
        }
        string script;

        public void CopyDataFrom(ContractScriptActivity source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            Script = source.Script;
        }

        public ContractScriptActivity WithMigrator(IMigrator parentMigrator)
        {
            Migrator = parentMigrator;
            return this;
        }
    }
}