"""Repository-specific classification decisions for SAM_Multitasker (the only non-shared tool file).

OVERRIDES     : component display name -> (object glyph, op, extra)   extra: None | "plural" | "library" | note
PARAM_OBJECTS : param type key (Goo<X>Param class or typeof(X) name) -> glyph | (glyph, container, plural)
OBJECTS/VERBS : extra noun/verb rules tried before the shared ones (same shapes as SAM's OBJECTS/VERBS)
"""
OVERRIDES = {
    "SAMMultitasker.Run": ("tasks", "run", None),
    "SAMMultitasker.DefaultScripts": ("script", "get", "library"),
    "SAMMultitasker.MultitaskerInput": ("value", "create", "multitasker input"),
    "SAMMultitasker.MultitaskerVariable": ("settings", "create", "multitasker variable (varied setting)"),
}
PARAM_OBJECTS = {"MultitaskerInput": "value", "MultitaskerOutput": "result", "MultitaskerVariable": "settings"}
OBJECTS = []
VERBS = []
