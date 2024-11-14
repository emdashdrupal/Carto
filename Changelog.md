# Carto changelog

## 0.3

* Now Carto can export points of interest (POIs).
  * The POI feature has the following spatial field: <u>Location</u>.
  * The POI feature has the following non-spatial fields: Address and Category.
* Added POI Category Format option (Miscellaneous Tab > POI Category Format). Users can select from two options: All and Single. The former exports all applicable categories of the POI, and the latter only exports the most applicable one; default to be All.
* Added Export Sub-Building Upgrades’ POIs option (Miscellaneous Tab > Export Sub-Building Upgrades’ POIs). Users can decide whether to export sub-building upgrades as individual POIs or not; default to be false.
* Added three ready-to-use QGIS style presets (Plan, Street & Topo); they can be accessed in the directory `ModsData\Carto\Styles`.

Note: <u>Underlined</u> fields are newly added.

## 0.2.5

* Fixed the issue where ArcGIS Pro could not read files containing periods (.) in the field titles. The separators for these fields are now replaced with underscores (_). [Reported by [load-ing](https://forum.paradoxplaza.com/forum/members/load-ing.1818892/)]
* Added two new tokens to use in the Custom Name field: {Date} & {Time}. The former represents the in-game date, and the latter represents the in-game time.

## 0.2.4

* Fixed the issue where the Edge field of the Network feature could not be read by software such as ArcGIS. [Reported by [Cities: Skylines LightLight](https://www.youtube.com/@CS_LightLight)]
* Fixed the jagged edges of some pathways connecting to the normal roads.
* Tried to fix the problem of the exported Edge field of the Network feature containing invalid polygons. Some micro (< 1m) self-intersections may still appear when the roads are connected at a very sharp angle.

## 0.2.3

* Fixed the bug where the export process stopped when at least one chosen feature doesn’t have matching entities. These features are now ignored and the user will be notified at the end of the export. [Reported by [@Allegretic](https://mods.paradoxplaza.com/authors/Allegretic)]

## 0.2.2

* Added four nonspatial fields to the Area features, including <u>Company</u>, Employee, Household, and Resident.
* Added one nonspatial field to Building features: Theme.
* Added User Manual button (General Tab > User Manual), which allows users to access the manual directly.
* Added Count Homeless Residents option (Miscellaneous Tab > Count Homeless Residents). Users can decide to neglect the homeless when calculating the number of households and residents; default to be true.
* Fix the bug that the mod can not export Shapefiles when the directory `ModsData\Carto\Shapefile` doesn’t exist.
* Remove UTF-8 BOM at the beginning of `.prj` files, so ArcGIS Pro can correctly identify the CRS of the Shapefiles.
* Added the export success dialog.
* Added support for the networks made by Road Builder mod.

Note: <u>Underlined</u> fields are newly added.

## 0.2.1

* Now Carto can export the world heightmap, which covers the area beyond the playable ones.
  * Added a new spatial field for Terrain: World Elevation.
  * Added a new spatial field for Water Bodies: World Depth.
* Added GeoTIFF Format option (Miscellaneous Tab > GeoTIFF Format). Users can choose from three options, they are Int16 (16-bit Integer), Norm16 (16-bit Normalized Number), and Float32 (32-bit Float Number); default to be Int16.
* Rewrite the method to export Shapefiles. Now the string field in the dBASE file is not fixed to 254 bytes but dynamically calculated, which can reduce the file size by 80% on average.
* Reorganize the options. Version is now added to the General Tab, and the Enable All Fields button is migrated to the Miscellaneous Tab.
* Added the export warning dialog, which is aimed at helping users avoid predictable errors.
* Added Simplified Chinese (zh-HANS) and Traditional Chinese (zh-HANT) translations.

## 0.2

* Now Carto can export buildings, users can configure them in the Custom Export Tab.
  * The Building feature has the following spatial field: Edge.
  * The Building feature has the following nonspatial fields: <u>Address</u>, Assets, <u>Brand</u>, <u>Employee</u>, <u>Household</u>, <u>Level</u>, Object, <u>Product</u>, <u>Resident</u>, and <u>Zoning</u>.
* The Category field of the zoning is now renamed as the Zoning field.
* Several generic classes migrated to Carto.Domain and Carto.Utils.ExportUtils namespaces.

Note: <u>Underlined</u> fields are newly added.

## 0.1.2

* Now Carto can export the spatial field of zonings (Edge).
  * Carto.Systems.ZoningSystem.GetZoningProperties() is now integrated into Carto.Utils.ExportUtils.
  * Remove the behavior of calling the method mentioned on the game loaded.
* Fix the bug that vanilla zone colors can not be exported once the Zone Color Changer is enabled.
* Added Miscellaneous Tab and two options：
  * Added Export Unzoned Zoning Cells option. Users can decide not to export empty cells to reduce the file size; default to be false.
  * Added Use Zone Color Changer’s Color option. Users can decide to export Zone Color Changer’s colors instead of vanilla’s; default to be true.

## 0.1.1

* Now Carto can export the nonspatial fields of zonings, including Category, <u>Color</u>, <u>Density</u>, Object, and <u>Theme</u>.
  * Carto.Systems.ZoningSystem.GetZoningProperties() method will be called once the game is loaded, which leaves the information of each zoning type.
  * Carto can not export zoning’s spatial field at the moment.
* By using System.Reflection, Carto can integrate the zoning color information set in the Zone Color Changer mod.

Note: <u>Underlined</u> fields are newly added.

## 0.1

* Carto’s first public repository version.
