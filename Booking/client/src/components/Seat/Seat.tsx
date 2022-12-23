import { useEffect, useState } from "react";
import { useSeat } from "@api/getSeat";
import { CircularProgress, Button, TextField } from "@mui/material";
import { useParams, useNavigate } from "react-router-dom";
import { Booking } from "@type/booking";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import {
  LocalizationProvider,
  PickersDay,
  PickersDayProps,
  StaticDatePicker,
  nbNO,
} from "@mui/x-date-pickers";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import dayjs, { Dayjs } from "dayjs";
import "dayjs/locale/nb";
import { nbNO as coreNbNO } from "@mui/material/locale";
import styles from "./Seat.module.css";
import { BookSeat } from "./BookSeat";

const theme = createTheme(
  {
    palette: {
      primary: { main: "#DF8B0D" },
    },
  },
  nbNO, // x-date-pickers translations
  coreNbNO // core translations
);

const getOccupiedDays = (bookings: Booking[], date: Dayjs): number[] => {
  let occupiedDaysInMonth: number[] = [];
  bookings.forEach((b: Booking) => {
    if (dayjs(b.date).month() == date.month()) {
      occupiedDaysInMonth.push(dayjs(b.date).date());
    }
  });
  return occupiedDaysInMonth;
};

export const Seat = () => {
  const { seatId } = useParams();
  const { isLoading, data, error } = useSeat(seatId!);
  const [date, setDate] = useState<Dayjs | null>(dayjs());
  const [occupiedDays, setOccupiedDays] = useState<number[]>();

  let navigate = useNavigate();

  useEffect(() => {
    if (data && date) {
      setOccupiedDays(getOccupiedDays(data.bookings, date));
    }
  }, [data]);

  if (isLoading) {
    return (
      <div style={{ display: "flex" }}>
        <CircularProgress size={100} style={{ margin: "10vh auto" }} />;
      </div>
    );
  }

  if (error) {
    return <h4>Kan ikke hente setet.</h4>;
  }

  const routeChange = () => {
    let path = `/`;
    navigate(path);
  };

  const renderOccupiedDays = (
    day: Dayjs,
    _date: Dayjs[],
    DayComponentProps: PickersDayProps<Dayjs>
  ) => {
    const isOccupied =
      !DayComponentProps.outsideCurrentMonth &&
      occupiedDays?.find((d) => d == day.date());
    return (
      <PickersDay
        sx={
          isOccupied
            ? {
                border: "solid #DF8B0D",
              }
            : undefined
        }
        {...DayComponentProps}
      />
    );
  };

  const handleMonthChange = (date: Dayjs) => {
    setOccupiedDays(getOccupiedDays(data!.bookings, date));
  };

  return (
    <>
      <ThemeProvider theme={theme}>
        <Button
          onClick={routeChange}
          variant="contained"
          style={{ backgroundColor: "#DF8B0D" }}
        >
          Tilbake
        </Button>
        <div
          style={{
            display: "flex",
            flexDirection: "column",
            overflow: "hidden",
            margin: "10px",
            minWidth: "80vw",
          }}
        >
          <BookSeat
            seat={data}
            date={date}
            booking={data?.bookings.find(
              (b) =>
                dayjs(b.date).format("YYYY-MM-DD") == date?.format("YYYY-MM-DD")
            )}
          />
        </div>
        <LocalizationProvider dateAdapter={AdapterDayjs} adapterLocale="nb">
          <StaticDatePicker
            value={date}
            onChange={(newValue) => {
              setDate(newValue);
            }}
            onMonthChange={handleMonthChange}
            renderInput={(params) => <TextField {...params} />}
            renderDay={renderOccupiedDays}
            closeOnSelect={false}
            showToolbar={false}
            disablePast={true}
            displayStaticWrapperAs="desktop"
            // shouldDisableYear={(year) => {
            //   if (year.year() > dayjs().year() + 1) return true;
            //   else return false;
            // }}
          />
        </LocalizationProvider>
      </ThemeProvider>
      {/* <div style={{display: "flex", flexDirection: "column", overflow: "hidden"}}>
          <div style={{margin: "0 auto"}}>
            {
              data?.bookings.map((b: any) => (
                <div key={b.id}>Date: {b.date}</div>
              ))
            }
          </div>
        </div> */}
    </>
  );
};
