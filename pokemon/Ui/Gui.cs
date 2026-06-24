using System;
using Gum.Forms.Controls;
using Gum.GueDeriving;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameGum;
using MonoGameLibrary;

namespace pokemon.Ui;

public class Gui
{
    private Panel _gui;

    public Gui() { }

    private void CreateGui()
    {
        _gui = new Panel();
        _gui.Dock(Gum.Wireframe.Dock.Fill);
        _gui.IsVisible = false;
        _gui.AddToRoot();

        var optionsText = new TextRuntime();
        optionsText.X = 10;
        optionsText.Y = 10;
        optionsText.Text = "OPTIONS";
        _gui.AddChild(optionsText);
    }
}
