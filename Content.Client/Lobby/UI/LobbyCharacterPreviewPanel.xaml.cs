using System.Numerics;
using Content.Client.UserInterface.Controls;
using Prometheus;
using Robust.Client.AutoGenerated;
using Robust.Client.UserInterface;
using Robust.Client.UserInterface.Controls;
using Robust.Client.UserInterface.XAML;

namespace Content.Client.Lobby.UI;

[GenerateTypedNameReferences]
public sealed partial class LobbyCharacterPreviewPanel : Control
{
    [Dependency] private readonly IEntityManager _entManager = default!;

    // public Button CharacterSetupButton => CharacterSetup;

    private EntityUid? _previewDummy;

    public LobbyCharacterPreviewPanel()
    {
        RobustXamlLoader.Load(this);
        IoCManager.InjectDependencies(this);
    }

    public void SetLoaded(bool value)
    {
        Loaded.Visible = value;
        Unloaded.Visible = !value;
    }

    public void SetSummaryText(string value)
    {
        Summary.Text = value;
    }

    public void SetSprite(EntityUid uid)
    {
        if (_previewDummy != null)
        {
            _entManager.DeleteEntity(_previewDummy);
        }

        _previewDummy = uid;

        ViewBox.DisposeAllChildren();
        var spriteView = new SpriteView
        {
            OverrideDirection = Direction.South,
            Scale = new Vector2(4f, 4f),
            MaxSize = new Vector2(112, 112),
            Stretch = SpriteView.StretchMode.Fill,
        };
        spriteView.SetEntity(uid);
        ViewBox.AddChild(spriteView);
    }

    protected override void Dispose(bool disposing)
    {
        base.Dispose(disposing);
        _entManager.DeleteEntity(_previewDummy);
        _previewDummy = null;
    }
}
