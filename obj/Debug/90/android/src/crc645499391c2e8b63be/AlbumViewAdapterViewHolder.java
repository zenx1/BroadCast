package crc645499391c2e8b63be;


public class AlbumViewAdapterViewHolder
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("BroadCast.Adapters.AlbumViewAdapterViewHolder, BroadCast", AlbumViewAdapterViewHolder.class, __md_methods);
	}


	public AlbumViewAdapterViewHolder ()
	{
		super ();
		if (getClass () == AlbumViewAdapterViewHolder.class)
			mono.android.TypeManager.Activate ("BroadCast.Adapters.AlbumViewAdapterViewHolder, BroadCast", "", this, new java.lang.Object[] {  });
	}

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
