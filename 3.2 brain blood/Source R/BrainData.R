library(readr)
brain <- read_table2("brain.data", col_names = FALSE, skip = 1)
View(brain)

brain <- brain[, -73]

BrainDataFrame = data.frame(brain)

BrainDensityFrame = BrainDataFrame[1:82 , ]

BrainBloodFrame = BrainDataFrame[82:164 , ]

library(plotly)

f1 <- list(
  family = "Arial, sans-serif",
  size = 16,
  color = "blue")

f2 <- list(
  family = "Arial, sans-serif",
  size = 10,
  color = "black")

axisForXAndY <- list(
  titlefont = f1,
  tickfont = f2,
  showgrid = F,
  tickwidth = 2,#black marker near number
  tickmode = "linear",
  dtick = 10,
  zeroline = FALSE,
  showline = FALSE,
  showticklabels = TRUE,
  showgrid = FALSE
  
)

axisForZ <- list(
  title = "Bone Density",
  titlefont = f1,
  tickfont = f2,
  showgrid = F,
  range = c(0, 1000),
  tickwidth = 2, #black marker near number
  tickmode = "linear",
  dtick = 200,
  zeroline = FALSE,
  showline = FALSE,
  showticklabels = TRUE,
  showgrid = FALSE
  
)

scene = list(
  xaxis = axisForXAndY,
  yaxis = axisForXAndY,
  zaxis = axisForZ
  ##camera = list(eye = list(x = -1.25, y = 1.25, z = 2.25))
  )

BoneDensity <- data.matrix(BrainDensityFrame)


p <- plot_ly(colorbar = list(title = "Blood Flow")) %>% 
  add_surface(z = ~BoneDensity,showscale = FALSE, surfacecolor = ~data.matrix(BrainBloodFrame), colorscale = list(c(0, "rgb(255, 0, 0)"), list(1, "rgb(0, 0, 255)")), hoverinfo = "name", name = "Bone Density") %>%
  add_surface(z = ~data.matrix(BrainBloodFrame), opacity = 0, colorscale = list(c(0, "rgb(255, 0, 0)"), list(1, "rgb(0, 255, 0)")), visible = TRUE, showlegend = FALSE,  hoverinfo = "none", name = "Blood Density") %>%
  layout(title = "Blood flow and bone desnity", scene = scene,autosize = TRUE)
  

p