# Martian GUI Controls

First aim for this library is playing with different GUI controls (primarily written in C#) for Windows on .NET Framework.

Second aim is to create some usable controls that will fill a lack of some functionality in standard controls available in .NET.

Currently the following controls are implemented.

## Virtual TreeView

* with immediate and delayed children loading
* it uses standard ability described at MSDN to show parent tree node like it has children using callbacks (and not creating fake child nodes)
