// SPDX-License-Identifier: LGPL-3.0-or-later
// Copyright (c) 2020-2026 Michal Dengusiak & Jakub Ziolkowski and contributors

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using NUnit.Framework;
using SAM.Core.Multitasker;
using RoslynDiagnostic = Microsoft.CodeAnalysis.Diagnostic;

[TestFixture]
public class MultitaskerDiagnosticsTests
{
    [Test]
    public void PublishedDiagnosticsApiStillUsesRoslynTypes()
    {
        Assert.That(typeof(MultitaskerResults).GetProperty(nameof(MultitaskerResults.Diagnostics)).PropertyType,
            Is.EqualTo(typeof(List<RoslynDiagnostic>)));
        Assert.That(typeof(MultitaskerResults).GetConstructor(new[] { typeof(IEnumerable<RoslynDiagnostic>) }),
            Is.Not.Null);
    }

    [Test]
    public void CompilationDiagnosticIsRetainedWithoutConversion()
    {
        RoslynDiagnostic diagnostic = RoslynDiagnostic.Create(
            new DiagnosticDescriptor("SAMREG001", "Compilation", "Invalid script", "Regression",
                Microsoft.CodeAnalysis.DiagnosticSeverity.Error, true), Location.None);
        var results = new MultitaskerResults(new[] { diagnostic });

        Assert.That(results.Succedded, Is.False);
        Assert.That(results.Diagnostics, Has.Count.EqualTo(1));
        Assert.That(results.Diagnostics[0], Is.SameAs(diagnostic));
    }

    [Test]
    public async Task InvalidScriptReturnsRoslynCompilationErrors()
    {
        MultitaskerResults results = await new Multitasker("this is not valid C#;").Run();

        Assert.That(results.Succedded, Is.False);
        Assert.That(results.Diagnostics.Any(d => d.Severity == Microsoft.CodeAnalysis.DiagnosticSeverity.Error), Is.True);
    }
}
