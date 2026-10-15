// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright (c) 2020-2026 Michal Dengusiak & Jakub Ziolkowski and contributors

using RoslynDiagnostic = Microsoft.CodeAnalysis.Diagnostic;
using System;
using System.Collections.Generic;

namespace SAM.Core.Multitasker
{
    public class MultitaskerResults
    {
        private List<RoslynDiagnostic> diagnostics;
        private List<MultitaskerResult> multitaskerResults;


        public MultitaskerResults(IEnumerable<RoslynDiagnostic> diagnostics)
        {
            multitaskerResults = null;
            this.diagnostics = diagnostics == null ? null : new List<RoslynDiagnostic> (diagnostics);
        }

        public MultitaskerResults(IEnumerable<MultitaskerResult> multitaskerResults)
        {
            this.multitaskerResults = multitaskerResults == null ? null : new List<MultitaskerResult>(multitaskerResults);
        }

        public bool Succedded
        {
            get
            {
                return diagnostics == null || diagnostics.Count == 0;
            }
        }

        public List<RoslynDiagnostic> Diagnostics
        {
            get
            {
                return diagnostics;
            }
        }

        public List<Exception> Exceptions
        {
            get
            {
                return multitaskerResults?.ConvertAll(x => x.Exception).FindAll(x => x != null);
            }
        }

        public List<T> GetOutputs<T>()
        {
            if (multitaskerResults == null)
            {
                return null;
            }

            List<T> result = new List<T>();
            foreach (MultitaskerResult multitaskerResult in multitaskerResults)
            {
                object @object = multitaskerResult?.MultitaskerOutput?.Result;
                if(@object is T)
                {
                    result.Add((T)@object);
                }
                else if(Core.Query.TryConvert(@object, out T t))
                {
                    result.Add((T)@object);
                }
            }

            return result;
        }
    }
}
