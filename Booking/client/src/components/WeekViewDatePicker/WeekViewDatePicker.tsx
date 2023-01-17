import React, { useState } from 'react';
import {
  format,
  startOfWeek,
  addDays,
  isSameDay,
  lastDayOfWeek,
  getWeek,
  addWeeks,
  subWeeks,
  isBefore,
} from 'date-fns';
import { nb } from 'date-fns/esm/locale';
import styles from './WeekViewDatePicker.module.css';
import { nbNO } from '@mui/x-date-pickers';
import { ThemeProvider, Button, Container, createTheme } from '@mui/material';
import { useAtom } from 'jotai';
import { dateAtom } from '../../jotaiProvider';

const theme = createTheme(
  {
    palette: {
      primary: { main: '#DF8B0D' },
    },
  },
  nbNO // x-date-pickers translations
);

const nor: Locale = nb;

export const WeekViewDatePicker = () => {
  const [date, setDate] = useAtom(dateAtom);
  const [selectedMonth, setSelectedMonth] = useState(date);
  const [selectedWeek, setSelectedWeek] = useState(getWeek(selectedMonth));

  const handleWeekChange = (btnType: string) => {
    if (btnType === 'prev') {
      setSelectedMonth(subWeeks(selectedMonth, 1));
      setSelectedWeek(getWeek(subWeeks(selectedMonth, 1)));
    }
    if (btnType === 'next') {
      setSelectedMonth(addWeeks(selectedMonth, 1));
      setSelectedWeek(getWeek(addWeeks(selectedMonth, 1)));
    }
  };

  const onDateChange = (day: Date) => {
    setDate(day);
  };

  const renderHeader = () => {
    const dateFormat = "MMMM";
    return (
      <Container className="text-center">
        <span>{format(selectedMonth, dateFormat, { locale: nor })}</span>
        <span className="mx-2">-</span>
        <span>
          uke {selectedWeek < 10 ? "0" + selectedWeek : selectedWeek}
        </span>
      </Container>
    );
  };

  const renderDays = () => {
    const dateFormat = "EEE";
    const days = [];
    let startDate = startOfWeek(selectedMonth, { weekStartsOn: 1 });
    for (let i = 0; i < 7; i++) {
      days.push(
        <div key={i} className={"w-full text-center"}>
          {format(addDays(startDate, i), dateFormat, { locale: nor })}
        </div>
      );
    }
    return <div className={"my-0 " + styles.days}>{days}</div>;
  };

  const renderCells = () => {
    const startDate = startOfWeek(selectedMonth, { weekStartsOn: 1 });
    const endDate = lastDayOfWeek(selectedMonth, { weekStartsOn: 1 });
    const dateFormat = "d";
    const rows = [];
    let days = [];
    let day: Date = startDate;
    let yesterday: Date = new Date();
    yesterday.setDate(yesterday.getDate() - 1);
    let formattedDate = "";
    while (day <= endDate) {
      for (let i = 0; i < 7; i++) {
        formattedDate = format(day, dateFormat, { locale: nor });
        const cloneDay = day;
        days.push(
          isBefore(day, yesterday) ? (
            <div className="w-full justify-center flex" key={day.getDate()}>
              <div
              className={"w-full " + styles.disabled + " " + styles.MuiButton}
              >
                <span>{formattedDate}</span>
              </div>
            </div>

          ) : (
            <div className="w-full justify-center flex" key={day.getDate()}>
              <div
                className={`${
                  isSameDay(day, date)
                    ? "w-full " + styles.selected + " " + styles.MuiButton
                    : isSameDay(day, new Date())
                    ? "w-full " + styles.today + " " + styles.MuiButton
                    : "w-full " + styles.MuiButton
                }`}
                onClick={() => {
                  onDateChange(cloneDay);
                }}
              >
                <span>{formattedDate}</span>
              </div>
            </div>
          )
        );
        day = addDays(day, 1);
      }

      rows.push(
        <div className={"w-full " + styles.cells} key={day.getDate()}>
          {days}
        </div>
      );
      days = [];
    }
    return rows;
  };

  const renderPrev = () => {
    return (
      <div>
        <Button
          sx={{ 
            minWidth: "24px",
            marginTop: "24px",
            color: "black"
          }}
          variant="outlined"
          size="small"
          onClick={() => handleWeekChange("prev")}
        >
          <svg xmlns="http://www.w3.org/2000/svg" height="24" width="24"><path d="m14 18-6-6 6-6 1.4 1.4-4.6 4.6 4.6 4.6Z"/></svg>
        </Button>
      </div>
    );
  };

  const renderNext = () => {
    return (
      <Button
        sx={{ 
          minWidth: "24px",
          marginTop: "24px",
          color: "black"
        }}
        className="absolute bottom-0 left-0"
        variant="outlined"
        size="small"
        onClick={() => handleWeekChange("next")}
      >
        <svg xmlns="http://www.w3.org/2000/svg" height="24" width="24"><path d="M9.4 18 8 16.6l4.6-4.6L8 7.4 9.4 6l6 6Z"/></svg>
      </Button>
    );
  };

  return (
    <>
      <ThemeProvider theme={theme}>
        <div>
          {renderHeader()}
          <div className="flex flex-row">
            <div className="flex-none ml-0 mr-1">{renderPrev()}</div>
            <div className="grow overflow-hidden w-full">
              {renderDays()}
              {renderCells()}
            </div>
            <div className="flex-none relative ml-1 mr-0">{renderNext()}</div>
          </div>
        </div>
      </ThemeProvider>
    </>
  );
};
