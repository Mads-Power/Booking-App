import dayjs, { Dayjs } from "dayjs";
import { SetStateAction, useEffect, useState } from "react";
import * as weekOfYear from "dayjs/plugin/weekOfYear";
dayjs.extend(weekOfYear);

import {
  format,
  subMonths,
  addMonths,
  startOfWeek,
  addDays,
  isSameDay,
  lastDayOfWeek,
  getWeek,
  addWeeks,
  subWeeks
} from "date-fns";

import styles from './WeekViewDatePicker.module.css';
import { MuiPickersAdapterContext, nbNO } from "@mui/x-date-pickers";
import { ThemeProvider, Button, Container, createTheme } from "@mui/material";

const theme = createTheme(
  {
    palette: {
      primary: { main: "#DF8B0D" },
    },
  },
  nbNO // x-date-pickers translations
);

const handleInitialDate = (date: Date) => {
  if (date) {
    console.log(date)
    return date;
  }
  else {
    return new Date();
  }
}

export const WeekViewDatePicker = ({...props}) => {
  const [currentMonth, setCurrentMonth] = useState(new Date());
  const [currentWeek, setCurrentWeek] = useState(getWeek(currentMonth));
  const [selectedDate, setSelectedDate] = useState(handleInitialDate(props.date));

  const handleWeekChange = (btnType: string) => {
  if (btnType === "prev") {
    setCurrentMonth(subWeeks(currentMonth, 1));
    setCurrentWeek(getWeek(subWeeks(currentMonth, 1)));
  }
  if (btnType === "next") {
    setCurrentMonth(addWeeks(currentMonth, 1));
    setCurrentWeek(getWeek(addWeeks(currentMonth, 1)));
  }
  };

  useEffect(() => {
    console.log("SELECTEDDATE")
    console.log(selectedDate)
  }, []);

  const onDateChange = (day: Date) => {
    setSelectedDate(day);
    props.handleSelectDate(day);
  };

  const renderHeader = () => {
    const dateFormat = "MMM yyyy";
    return (
      <Container>
          <span>{format(currentMonth, dateFormat)}</span>
      </Container>
    );
  };

    const renderDays = () => {
      const dateFormat = "EEE";
      const days = [];
      let startDate = startOfWeek(currentMonth, { weekStartsOn: 1 });
      for (let i = 0; i < 7; i++) {
        days.push(
          <Container key={i} className={styles.day}>
            {format(addDays(startDate, i), dateFormat)}
          </Container>
        );
      }
      return <div className={styles.days}>{days}</div>;
    };

    const renderCells = () => {
      const startDate = startOfWeek(currentMonth, { weekStartsOn: 1 });
      const endDate = lastDayOfWeek(currentMonth, { weekStartsOn: 1 });
      const dateFormat = "d";
      const rows = [];
      let days = [];
      let day: Date = startDate;
      let formattedDate = "";
      while (day <= endDate) {
        for (let i = 0; i < 7; i++) {
          formattedDate = format(day, dateFormat);
          const cloneDay = day;
          days.push(
            <Container
            // variant="outlined"
            className={
              `${
                isSameDay(day, selectedDate)
                ? styles.selected + " " + styles.MuiButton
                : isSameDay(day, new Date())
                  ? styles.today + " " + styles.MuiButton
                  : styles.MuiButton}`}
              key={day.getDate()}
              onClick={() => {
                onDateChange(cloneDay);
              }}
            >
              <span>{formattedDate}</span>
            </Container>
          );
          day = addDays(day, 1);
        }
  
        rows.push(
          <div className={styles.cells} key={day.getDate()}>
            {days}
          </div>
        );
        days = [];
      }
      return <div>{rows}</div>;
    };

    const renderFooter = () => {
      return (
        <Container>
            <Button variant="outlined" onClick={() => handleWeekChange("prev")}>
            {'<'}
            </Button>
          <span style={{margin: "0 10px"}}>Week {currentWeek< 10 ? "0"+currentWeek : currentWeek}</span>
          <Button variant="outlined" onClick={() => handleWeekChange("next")}>
            {'>'}
          </Button>
        </Container>
      );
    };

  return (
    <>
    <ThemeProvider theme={theme}>
        <Container className={styles.weekViewDatePicker}>
        {renderHeader()}
        <div className={styles.dayCellWrapper}>
        {renderDays()}
        {renderCells()}
        </div>
        {renderFooter()}
        </Container>
      </ThemeProvider>
    </>
    
  );
};