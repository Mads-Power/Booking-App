import { useEffect, useState } from "react";

import {
  format,
  startOfWeek,
  addDays,
  isSameDay,
  lastDayOfWeek,
  getWeek,
  addWeeks,
  subWeeks,
  isBefore
} from "date-fns";

import {nb} from "date-fns/esm/locale"

import styles from './WeekViewDatePicker.module.css';
import { nbNO } from "@mui/x-date-pickers";
import { ThemeProvider, Button, Container, createTheme } from "@mui/material";
import { DateContextType, useDateContext } from "@components/Provider/DateContextProvider";

const theme = createTheme(
  {
    palette: {
      primary: { main: "#DF8B0D" },
    },
  },
  nbNO // x-date-pickers translations
);

const nor: Locale = nb;

export const WeekViewDatePicker = () => {
  const { selectedDate, setSelectedDate }: DateContextType = useDateContext();
  const [selectedMonth, setSelectedMonth] = useState(selectedDate);
  const [selectedWeek, setSelectedWeek] = useState(getWeek(selectedMonth));


  const handleWeekChange = (btnType: string) => {
  if (btnType === "prev") {
    setSelectedMonth(subWeeks(selectedMonth, 1));
    setSelectedWeek(getWeek(subWeeks(selectedMonth, 1)));
  }
  if (btnType === "next") {
    setSelectedMonth(addWeeks(selectedMonth, 1));
    setSelectedWeek(getWeek(addWeeks(selectedMonth, 1)));
  }
  };

  useEffect(() => {
  }, []);

  const onDateChange = (day: Date) => {
    setSelectedDate(day);
  };

  const renderHeader = () => {
    const dateFormat = "MMM yyyy";
    return (
      <Container>
          <span>{format(selectedMonth, dateFormat, {locale: nor})}</span>
      </Container>
    );
  };

    const renderDays = () => {
      const dateFormat = "EEE";
      const days = [];
      let startDate = startOfWeek(selectedMonth, { weekStartsOn: 1 });
      for (let i = 0; i < 7; i++) {
        days.push(
          <Container key={i} className={styles.day}>
            {format(addDays(startDate, i), dateFormat, {locale: nor})}
          </Container>
        );
      }
      return <div className={styles.days}>{days}</div>;
    };

    const renderCells = () => {
      const startDate = startOfWeek(selectedMonth, { weekStartsOn: 1 });
      const endDate = lastDayOfWeek(selectedMonth, { weekStartsOn: 1 });
      const dateFormat = "d";
      const rows = [];
      let days = [];
      let day: Date = startDate;
      let yesterday: Date = new Date();
      yesterday.setDate(yesterday.getDate() - 1 );
      let formattedDate = "";
      while (day <= endDate) {
        for (let i = 0; i < 7; i++) {
          formattedDate = format(day, dateFormat, {locale: nor});
          const cloneDay = day;
          days.push(
            isBefore(day, yesterday) ?
            <Container
            className={styles.disabled + " " + styles.MuiButton} key={day.getDate()}>
              <span>{formattedDate}</span>
            </Container>
            :
            <Container
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
          <span style={{margin: "0 10px"}}>uke {selectedWeek< 10 ? "0" + selectedWeek : selectedWeek}</span>
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