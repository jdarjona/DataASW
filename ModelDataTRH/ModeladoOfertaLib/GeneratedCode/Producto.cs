﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó mediante una herramienta.
//     Los cambios del archivo se perderán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;





public class Producto
{
	public virtual string alias
	{
		get;
		set;
	}

    
    public virtual Decimal descripcion
	{
		get;
		set;
	}

	public virtual string idProducto
	{
		get;
		set;
	}

	public virtual int stockPAQ
	{
		get;
		set;
	}

	public virtual int cantidad
	{
		get;
		set;
	}

	public virtual Decimal kg_m2
	{
		get;
		set;
	}

	public virtual int panos_PAQ
	{
		get;
		set;
	}

	public virtual Decimal m2_PAQ
	{
		get;
		set;
	}

	public virtual string idTipoMalla
	{
		get;
		set;
	}

	public virtual Decimal largo
	{
		get;
		set;
	}

	public virtual Decimal barrasTrans
	{
		get;
		set;
	}

	public virtual Decimal barrasLong
	{
		get;
		set;
	}

	public virtual Decimal altura
	{
		get;
		set;
	}

	public virtual Decimal peso
	{
		get;
		set;
	}

	public virtual Decimal pila1
	{
		get;
		set;
	}

	public virtual Decimal pila2
	{
		get;
		set;
	}

	public virtual Decimal precioBase
	{
		get;
		set;
	}

	public virtual Decimal euro_TM
	{
		get;
		set;
	}

	public virtual Decimal euro_M2
	{
		get;
		set;
	}

	public virtual Decimal importe
	{
		get;
		set;
	}

	public virtual string tipoMalla
	{
		get;
		set;
	}

	public virtual List<Producto> getProductos(string idTipoMalla)
	{
		throw new System.NotImplementedException();
	}

}

