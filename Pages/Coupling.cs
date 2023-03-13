﻿using BlazorApp4.Shared;
using Microsoft.AspNetCore.Components;

public class Coupling
{
    private Dictionary<string, double> posX = new Dictionary<string, double> { { "I", 6 }, { "O", 93.5 }, { "T", 28 }, { "C", 71.5 }, { "P", 28 }, { "R", 71.5 } };
    private Dictionary<string, double> posY = new Dictionary<string, double> { { "I", 50 }, { "O", 50 }, { "T", 12 }, { "C", 12 }, { "P", 88 }, { "R", 88 } };
    private List<string> _displayText;
    private string _label;
    private double _labelDx;
    private double _labelDy;
    public Coupling(string Name, double labelDx, double labelDy, string directionX, string directionY, string notGroup, string outputFn, string label, string toFn, string toType,
        double drawTox, double drawToy, double drawFromx, double drawFromy)
    {
        this.Name = Name;
        _labelDx = labelDx;
        _labelDx = labelDy;
        this.directionX = directionX;
        this.directionY = directionY;
        this.notGroup = notGroup;
        this.outputFn = outputFn;
        this.toFn = toFn;
        _label = label;
        _displayText = returnTextLines(_label, 15);
        this.toType = toType;
        this.drawTox = drawTox;
        this.drawToy = drawToy;
        this.drawFromx = drawFromx;
        this.drawFromy = drawFromy;
    }
    public string Name { get; set; }
    public double labelDx
    {
        get { return _labelDx; }
        set
        {
            _labelDx = value;
            resetLabel();
        }
    }
    public double labelDy
    {
        get { return _labelDy; }
        set
        {
            _labelDy = value;
            resetLabel();
        }
    }
    public string directionX { get; set; }
    public string directionY { get; set; }
    public string notGroup { get; set; }
    public string outputFn { get; set; }
    public string toFn { get; set; }
    public string label
    {
        get { return _label; }
        set { _label = value;
            _displayText = returnTextLines(_label, 15);
            resetLabel();
        }
    }
    public double labelX { get; set; }
    public double labelY { get; set; }
    public double Twidth { get; set; }
    public string toType { get; set; }
    public double drawTox { get; set; }
    public double drawToy { get; set; }
    public double drawFromx { get; set; }
    public double drawFromy { get; set; }
    public double drawAx { get; set; }
    public double drawAy { get; set; }
    public double drawBx { get; set; }
    public double drawBy { get; set; }
    public double drawIntx { get; set; }
    public double drawInty { get; set; }
    public List<string> displayText
    {
        get { return _displayText; }
        set { _displayText = value; }
    }
    private List<string> returnTextLines(string text, int length)
    {
        var textLines = new List<string>();
        string[] textWords = text.Split(" ");
        int tL = 0;
        textLines.Add("");
        int lL = length;
        foreach (string tWord in textWords)
        {
            var doTWord = tWord;
            if (doTWord.StartsWith(Environment.NewLine))
            {
                textLines.Add("");
                tL++;
                lL = length;
                doTWord = tWord.Replace(Environment.NewLine, "");
            }
            if (doTWord.Contains(Environment.NewLine) && !doTWord.EndsWith(Environment.NewLine))
            {
                if (lL < length)
                {
                    textLines[tL] += " ";
                }
                string[] splitLines = doTWord.Split(Environment.NewLine);
                int sLi = 0;
                foreach (string splitWord in splitLines)
                {
                    doTWord = splitWord;
                    if (sLi < splitLines.Length - 1)
                    {
                        textLines[tL] += splitWord;
                        textLines.Add("");
                        tL++;
                        lL = length;
                    }
                    sLi++;
                }
            }
            if (doTWord.Length <= lL)
            {
                if (lL < length)
                {
                    textLines[tL] += " ";
                }
                textLines[tL] += doTWord;
                lL -= doTWord.Length;
                if (doTWord.EndsWith(Environment.NewLine))
                {
                    textLines.Add("");
                    tL++;
                    lL = length;
                }
            }
            else if (doTWord.Length > length)
            {
                string tempWord = doTWord;
                while (tempWord.Length > 0)
                {
                    textLines.Add("");
                    tL++;
                    lL = length;
                    string addWord = tempWord.Substring(0, Math.Min(length + 1, tempWord.Length));
                    textLines[tL] += addWord;
                    lL -= addWord.Length;
                    tempWord = tempWord.Substring(addWord.Length);
                }
            }
            else
            {
                textLines.Add("");
                tL++;
                textLines[tL] += doTWord;
                lL = length - doTWord.Length;
            }
        }
        return textLines;
    }
    public string curve
    {
        get
        {
            string[] _curve = new string[10]
            {
            drawTox.ToString("#.##"),
            drawToy.ToString("#.##"),
            drawFromx.ToString("#.##"),
            drawFromy.ToString("#.##"),
            drawAx.ToString("#.##"),
            drawAy.ToString("#.##"),
            drawBx.ToString("#.##"),
            drawBy.ToString("#.##"),
            drawIntx.ToString("#.##"),
            drawInty.ToString("#.##")
            };
            return string.Join("|", _curve);
        }
    }
    public string curve2
    {
        get
        {
            string[] _curve2 = new string[13] {
            "M", drawFromx.ToString("#.##"), drawFromy.ToString("#.##"),
            "Q", drawAx.ToString("#.##"), drawAy.ToString("#.##"), drawIntx.ToString("#.##"), drawInty.ToString("#.##"),
            "Q", drawBx.ToString("#.##"), drawBy.ToString("#.##"), drawTox.ToString("#.##"), drawToy.ToString("#.##")
            };
            return string.Join(" ", _curve2);
        }
    }
    public void resetLabel()
    {
        double tempRwidth = 0;
        if (directionX == "from")
        {
            labelX = (drawIntx + labelDx * (drawFromx - drawIntx));
            for (int i = 0; i < _displayText.Count; i++)
            {
                tempRwidth = Math.Max(tempRwidth, _displayText[i].Length);
            }
        }
        else
        {
            labelX = (drawIntx + labelDx * (drawIntx - drawTox));
            for (int i = 0; i < _displayText.Count; i++)
            {
                tempRwidth = Math.Max(tempRwidth, _displayText[i].Length);
            }
        }
        Twidth = tempRwidth * 4 + 4;
        if (directionY == "from")
        {
            labelY = (drawInty + labelDy * (drawFromy - drawInty) - (2.5 + _displayText.Count * 4));
        }
        else
        {
            labelY = (drawInty + labelDy * (drawInty - drawToy) - (2.5 + _displayText.Count * 4));
        }
    }
    public string reDrawLines(string IDNr, double fnX, double fnY)
    {
        //this function gets all the line coordinates
        //draws two quadratic Bezier curves to connect an Output with another Aspect
        //draws from {drawFrom} to {drawTo} through intermediate point {drawInt}
        //{drawA} and {drawB} are control points to create the curve. For a smooth curve {drawA}, {drawB} and {drawInt} must all be on the same line
        if (outputFn == IDNr)
        {
            drawTox = fnX + posX["O"];
            drawToy = fnY + posY["O"];
        }
        else if (IDNr !="")
        {
            drawFromx = fnX + posX[toType];
            drawFromy = fnY + posY[toType];
        }
        double deltaX = (drawTox - drawFromx);
        double deltaY = (drawToy - drawFromy);
        if (toType == "I")
        { //does a fn1 Output connect with a fn2 Input
            if (deltaX < 0)
            {
                drawAx = drawFromx + deltaX * 0.25;
                drawAy = drawFromy;
                drawBx = drawFromx + deltaX * 0.75;
                drawBy = drawToy;
            }
            else
            {
                if (Math.Abs(deltaY) > 120)
                {
                    drawAx = drawFromx - 20;
                    drawAy = drawFromy + deltaY * 0.375;
                    drawBx = drawTox + 20;
                    drawBy = drawFromy + deltaY * 0.625;
                }
                else
                {
                    drawAx = drawFromx - 20;
                    drawAy = drawFromy + deltaY * 0.5;
                    drawBx = drawTox + 20;
                    drawBy = drawFromy + deltaY * 0.5;
                }
            }
            drawIntx = drawFromx + deltaX * 0.5;
            drawInty = drawFromy + deltaY * 0.5;
        }
        else if (toType == "T")
        { //does a fn1 output connect with a fn2 Time
            if (deltaX < 0)
            {
                if (deltaY < 0)
                {
                    drawAx = drawFromx + deltaX * 0.25;
                    drawAy = drawFromy + deltaY * 0.5;
                    drawBx = drawFromx + deltaX * 0.75;
                    drawBy = drawToy;
                    drawIntx = drawFromx + deltaX * 0.5;
                    drawInty = drawFromy + deltaY * 0.75;
                }
                else
                {
                    drawAx = drawFromx + deltaX * 0.5;
                    drawAy = drawFromy;
                    drawBx = drawFromx + deltaX * 0.75;
                    drawBy = drawToy;
                    drawIntx = drawFromx + deltaX * 0.625;
                    drawInty = drawFromy + deltaY * 0.5;
                }
            }
            else
            {
                if (deltaY < 0)
                {
                    if (Math.Abs(deltaY) > 120)
                    {
                        drawAx = drawFromx - 20;
                        drawAy = drawFromy + deltaY * 0.125;
                        drawBx = drawTox + 20;
                        drawBy = drawFromy + deltaY * 0.375;
                        drawIntx = drawFromx + deltaX * 0.5;
                        drawInty = drawFromy + deltaY * 0.25;
                    }
                    else
                    {
                        drawAx = drawFromx - 20;
                        drawAy = drawFromy + deltaY * 0.25;
                        drawBx = drawTox + 20;
                        drawBy = drawFromy + deltaY * 0.25;
                        drawIntx = drawFromx + deltaX * 0.5;
                        drawInty = drawFromy + deltaY * 0.25;
                    }
                }
                else
                {
                    drawAx = drawFromx;
                    drawAy = drawFromy - 20;
                    drawBx = drawTox + 20;
                    drawBy = drawFromy - 20;
                    drawIntx = drawFromx + deltaX * 0.25;
                    drawInty = drawFromy - 20;
                }
            }
        }
        else if (toType == "C")
        { //does a fn1 output connect with a fn2 Control
            if (deltaX < 0)
            {
                if (deltaY < -20)
                {
                    drawAx = drawFromx;
                    drawAy = drawFromy + deltaY * 0.5;
                    drawBx = drawFromx + deltaX * 0.5;
                    drawBy = drawToy;
                    drawIntx = drawFromx + deltaX * 0.25;
                    drawInty = drawFromy + deltaY * 0.75;
                }
                else
                {
                    drawAx = drawFromx;
                    drawAy = drawFromy - 20;
                    drawBx = drawTox + 20;
                    drawBy = drawFromy - 20;
                    drawIntx = drawFromx + deltaX * 0.25;
                    drawInty = drawFromy - 20;
                }
            }
            else
            {
                drawAx = drawFromx + deltaX * 0.5 + 20;
                drawAy = drawFromy;
                drawBx = drawTox + 20;
                drawBy = drawFromy + deltaY * 0.5;
                drawIntx = drawFromx + deltaX * 0.75 + 20;
                drawInty = drawFromy + deltaY * 0.25;
            }
        }
        else if (toType == "P")
        { //does a fn1 output connect with a fn2 Precondition
            if (deltaX < 0)
            {
                if (deltaY < 0)
                {
                    drawAx = drawFromx + deltaX * 0.5;
                    drawAy = drawFromy;
                    drawBx = drawFromx + deltaX * 0.75;
                    drawBy = drawToy;
                    drawIntx = drawFromx + deltaX * 0.625;
                    drawInty = drawFromy + deltaY * 0.5;
                }
                else
                {
                    drawAx = drawFromx + deltaX * 0.25;
                    drawAy = drawFromy + deltaY * 0.5;
                    drawBx = drawFromx + deltaX * 0.75;
                    drawBy = drawToy;
                    drawIntx = drawFromx + deltaX * 0.5;
                    drawInty = drawFromy + deltaY * 0.75;
                }
            }
            else
            {
                if (deltaY < 0)
                {
                    drawAx = drawFromx;
                    drawAy = drawFromy + 20;
                    drawBx = drawTox + 20;
                    drawBy = drawFromy + 20;
                    drawIntx = drawFromx + deltaX * 0.25;
                    drawInty = drawFromy + 20;
                }
                else
                {
                    if (Math.Abs(deltaY) > 120)
                    {
                        drawAx = drawFromx - 20;
                        drawAy = drawFromy + deltaY * 0.125;
                        drawBx = drawTox + 20;
                        drawBy = drawFromy + deltaY * 0.375;
                        drawIntx = drawFromx + deltaX * 0.5;
                        drawInty = drawFromy + deltaY * 0.25;
                    }
                    else
                    {
                        drawAx = drawFromx - 20;
                        drawAy = drawFromy + deltaY * 0.25;
                        drawBx = drawTox + 20;
                        drawBy = drawFromy + deltaY * 0.25;
                        drawIntx = drawFromx + deltaX * 0.5;
                        drawInty = drawFromy + deltaY * 0.25;
                    }
                }
            }
        }
        else if (toType == "R")
        { //does a fn1 output connect with a fn2 Resource
            if (deltaX < 0)
            {
                if (deltaY < 20)
                {
                    drawAx = drawFromx;
                    drawAy = drawFromy + 20;
                    drawBx = drawTox + 20;
                    drawBy = drawFromy + 20;
                    drawIntx = drawFromx + deltaX * 0.25;
                    drawInty = drawFromy + 20;
                }
                else
                {
                    drawAx = drawFromx;
                    drawAy = drawFromy + deltaY * 0.5;
                    drawBx = drawFromx + deltaX * 0.5;
                    drawBy = drawToy;
                    drawIntx = drawFromx + deltaX * 0.25;
                    drawInty = drawFromy + deltaY * 0.75;
                }
            }
            else
            {
                drawAx = drawFromx + deltaX * 0.5 + 20;
                drawAy = drawFromy;
                drawBx = drawTox + 20;
                drawBy = drawFromy + deltaY * 0.5;
                drawIntx = drawFromx + deltaX * 0.75 + 20;
                drawInty = drawFromy + deltaY * 0.25;
            }
        }

        string[] curve2 = new string[13] {
            "M", drawFromx.ToString("#.##"), drawFromy.ToString("#.##"),
            "Q", drawAx.ToString("#.##"), drawAy.ToString("#.##"), drawIntx.ToString("#.##"), drawInty.ToString("#.##"),
            "Q", drawBx.ToString("#.##"), drawBy.ToString("#.##"), drawTox.ToString("#.##"), drawToy.ToString("#.##")
        };
        resetLabel();
        return string.Join(" ", curve2);
    }
}