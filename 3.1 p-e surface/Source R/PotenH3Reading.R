library(readr)
potenh3 <- read_csv("potenh3.data", trim_ws = FALSE, col_names = FALSE,
                     na = "null", skip = 1)
 
Energy = matrix(NA, 71, 71, byrow = TRUE)
rowcount = 1
columncount = 1
CurrentRowDataPointer = 0
minValue = 0
localminValue = 0
localminValueX = 0
localminValueY = 0
while(rowcount <= 71){
    for(k in 1:11){
      CurrentRow <- as.list(strsplit(as.character(potenh3[k+CurrentRowDataPointer, 1]), '\\s+')[[1]])
      CurrentRow <- CurrentRow[-1]
     
      for(l in 1:7){
        if(columncount <= 71){
          Energy[rowcount,columncount] = strtoi(CurrentRow[l]) 
          if(minValue > strtoi(CurrentRow[l]) ){
            minValue = strtoi(CurrentRow[l])
          }
          if(rowcount < 10 && columncount < 10){
            if(localminValue > strtoi(CurrentRow[l]))
              localminValue = strtoi(CurrentRow[l])
              localminValueX = rowcount
              localminValueY = columncount
          }
          columncount = columncount + 1
        }
      }
    }
    CurrentRowDataPointer = CurrentRowDataPointer + 12
    columncount = 1
    rowcount = rowcount + 1
}

View(Energy)

B = matrix(0, 71, 71, byrow = TRUE)
C = matrix(minValue, 71, 71, byrow = TRUE)
D = matrix(localminValue, 71, 71, byrow = TRUE)

library(plotly)
p <- plot_ly(colorbar = list(title = "Energy")) %>% 
      add_surface(z = ~Energy, colors=c("red","grey","grey","grey"), hoverinfo = "x+y+z") %>% 
      add_surface(z = ~B, opacity = 0.51, showscale = FALSE, name = "ZeroPlane", hoverinfo = "none",color="Blue") %>%
      add_markers(z = localminValue, x = localminValueX, y = localminValueY,surfacecolor = "Red", showlegend = FALSE, name = paste("Local Minimum Value: " , as.character(localminValue)), hoverinfo = "name") %>%    
      layout(title = '\n\rPotential-energy surface of the chemical reaction \n\r H + H2 ↔ H2 + H')
      
p

