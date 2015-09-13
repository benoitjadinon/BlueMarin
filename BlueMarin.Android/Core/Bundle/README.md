
**BundleExtensions**

BEFORE :

need to know the property type and call the right getXXX and putXXX

need for a constant as well

	public static final String SEL_CAL_ID = "SELCALLID";

	int SelectedCalendarID { get; set; }

	protected override void OnCreate (Bundle savedInstanceState)
	{
		base.OnCreate (savedInstanceState);

		SelectedCalendarID = savedInstanceState.getInt(SEL_CAL_ID);
	}

	protected override void OnSaveInstanceState (Bundle outState)
	{
		base.OnSaveInstanceState (outState);

		outState.putInt(SEL_CAL_ID, SelectedCalendarID)
	}


AFTER :

type is infered 

key name is read from the property itself

	int SelectedCalendarID { get; set; }

	protected override void OnCreate (Bundle savedInstanceState)
	{
		base.OnCreate (savedInstanceState);

		savedInstanceState.FillValue(() => SelectedCalendarID, -1);
	}

	protected override void OnSaveInstanceState (Bundle outState)
	{
		base.OnSaveInstanceState (outState);

		outState.PutValue(() => SelectedCalendarID);
	}