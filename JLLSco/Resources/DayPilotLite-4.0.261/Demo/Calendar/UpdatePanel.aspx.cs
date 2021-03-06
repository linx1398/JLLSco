using System;
using System.Data;
using DayPilot.Web.Ui.Events;

public partial class UpdatePanel : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Calendar1.SelectedDate = DateTime.Today;
            setWeek();

            DayPilotCalendar1.DataSource = getData();
            DataBind();
        }

    }

    protected DataTable getData()
    {
        DataTable dt;
        dt = new DataTable();
        dt.Columns.Add("start", typeof(DateTime));
        dt.Columns.Add("end", typeof(DateTime));
        dt.Columns.Add("name", typeof(string));
        dt.Columns.Add("id", typeof(string));

        DataRow dr;

        dr = dt.NewRow();
        dr["id"] = 0;
        dr["start"] = Convert.ToDateTime("15:50");
        dr["end"] = Convert.ToDateTime("15:55");
        dr["name"] = "Event 1";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 1;
        dr["start"] = Convert.ToDateTime("16:00");
        dr["end"] = Convert.ToDateTime("17:00");
        dr["name"] = "Event 2";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 2;
        dr["start"] = Convert.ToDateTime("16:15");
        dr["end"] = Convert.ToDateTime("18:45");
        dr["name"] = "Event 3";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 3;
        dr["start"] = Convert.ToDateTime("16:30");
        dr["end"] = Convert.ToDateTime("17:30");
        dr["name"] = "Sales Dept. Meeting Once Again";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 4;
        dr["start"] = Convert.ToDateTime("8:00");
        dr["end"] = Convert.ToDateTime("9:00");
        dr["name"] = "Event 4";
        dt.Rows.Add(dr);

        dr = dt.NewRow();
        dr["id"] = 5;
        dr["start"] = Convert.ToDateTime("22:00");
        dr["end"] = Convert.ToDateTime("6:00").AddDays(1);
        dr["name"] = "Event 5";
        dt.Rows.Add(dr);


        dr = dt.NewRow();
        dr["id"] = 6;
        dr["start"] = Convert.ToDateTime("11:00");
        dr["end"] = Convert.ToDateTime("13:00");
        dr["name"] = "Event 6";
        dt.Rows.Add(dr);

        return dt;

    }

    protected void DayPilotCalendar1_EventClick(object sender, EventClickEventArgs e)
    {
        //Label1.Text = e.Value;
    }
    protected void DayPilotCalendar1_TimeRangeSelected(object sender, TimeRangeSelectedEventArgs e)
    {
    }
    protected void Calendar1_SelectionChanged(object sender, EventArgs e)
    {
        setWeek();
    }

    private void setWeek()
    {
        DateTime firstDay = firstDayOfWeek(Calendar1.SelectedDate, DayOfWeek.Sunday);
        Calendar1.VisibleDate = firstDay;
        for (int i = 0; i < 7; i++)
            Calendar1.SelectedDates.Add(firstDay.AddDays(i));

        DayPilotCalendar1.StartDate = firstDay;
    }

    /// <summary>
    /// Gets the first day of a week where day (parameter) belongs. weekStart (parameter) specifies the starting day of week.
    /// </summary>
    /// <returns></returns> 
    private static DateTime firstDayOfWeek(DateTime day, DayOfWeek weekStarts)
    {
        DateTime d = day;
        while (d.DayOfWeek != weekStarts)
        {
            d = d.AddDays(-1);
        }

        return d;
    }
}
