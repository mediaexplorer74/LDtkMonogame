namespace LDtk.ContentPipeline.Level;

using System;
using System.Text.Json;

using LDtk;

using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

[ContentTypeWriter]
public class LDtkLevelWriter : ContentTypeWriter<LDtkLevel>
{
    protected override void Write(ContentWriter output, LDtkLevel value)
    {
        if (output == null)
        {
            throw new ArgumentNullException(nameof(output));
        }
        output.Write(JsonSerializer.Serialize(value, Constants.SerializeOptions));
    }

    public override string GetRuntimeReader(TargetPlatform targetPlatform)
    {
        return "LDtk.ContentPipeline.LDtkLevelReader, LDtkMonogame";
    }
}
