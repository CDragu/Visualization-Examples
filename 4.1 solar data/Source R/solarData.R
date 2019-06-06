library(readxl)
inverter_full <- read_excel("inverter-full.xlsx", 
                            sheet = "Power_w", col_names = FALSE, 
                            col_types = c("date", "numeric"), na = "NULL")

colnames(inverter_full) <- c("Date", "Power")

View(inverter_full)

A <- as.data.frame(inverter_full[2,1])
A <- (strsplit(as.character(as.data.frame(inverter_full[,1])[, 1]) ,' '))



library(chron)

DataMatrix = matrix(0, nrow = length(A), ncol = 3, byrow = TRUE)

for (i in 1:(length(A))) {
  DataMatrix[i, 1] = as.character(unlist(A[i])[1])
  
  timeInMinutes = 60 * 24 * as.numeric(times(as.character(unlist(A[i])[2])))
  if(timeInMinutes %% 5 == 0){
    
    
    DataMatrix[i, 2] = as.character(unlist(A[i])[2])
  
    DataMatrix[i, 3] = as.numeric(inverter_full[i,2][1]) 
  }else{
    DataMatrix[i, 1] = ""
    
    DataMatrix[i, 2] = ""
  
    DataMatrix[i, 3] = ""
  }
 
  
}

colnames(DataMatrix) <- c("Date","Time","Power")


DataFrame = data.frame(DataMatrix[, 1], DataMatrix[, 2], DataMatrix[, 3])
colnames(DataFrame) <- c("Date","Time","Power")

library(plotly)
library(reshape2)

Energy <- as.numeric(as.character(DataFrame[,3]))
Date <- as.Date(DataFrame[, 1])
Hour <- as.character(DataFrame[,2])

p <- plot_ly(
  x = Date, 
  y = Hour,
  z = Energy, 
  type = "heatmap", 
  colors = colorRamp(c("steelblue4","steelblue3","steelblue2", "steelblue1")),
  colorbar = list(title = "Solar-Power")
) %>%
  layout(title = "Solar-Power over a year")
p

