using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal interface Movable
{
    public bool move(int dx, int dy);
    public void setPos(int x, int y);
}
